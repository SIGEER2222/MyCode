using Microsoft.CodeAnalysis;

namespace SourceGenerator
{
    [Generator]
    public class HelloSourceGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            // Find the main method
            //Microsoft.CodeAnalysis.Compilation.GetEntryPoint 方法返回程序的入口点的 IMethodSymbol
            var mainMethod = context.Compilation.GetEntryPoint(context.CancellationToken);

            // Build up the source code
            string source = $@" // Auto-generated code
using System;

namespace {mainMethod.ContainingNamespace.ToDisplayString()}
{{
    public static partial class {mainMethod.ContainingType.Name}
    {{
        static partial void HelloFrom(string name) =>
            Console.WriteLine($""Generator says: Hi from '{{name}}'"");
    }}
}}
";
            var typeName = mainMethod.ContainingType.Name;

            // Add the source code to the compilation
            //GeneratorExecutionContext.AddSource 方法中的 hintName 参数可以是任何唯一名称。 通常为该名称提供显式 C# 文件扩展名，例如 ".g.cs" 或 ".generated.cs"。 该文件名有助于将文件标识为正在生成源。
            context.AddSource($"{typeName}.g.cs", source);
        }

        public void Initialize(GeneratorInitializationContext context)
        {
            // No initialization required for this one
        }
    }
}