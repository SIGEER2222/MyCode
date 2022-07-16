using System.Net.Http;

string urlA = "https://docs.microsoft.com/zh-cn/microsoft-edge/extensions-chromium/getting-started/part2-content-scripts?tabs=v2";
string urlB = "https://docs.microsoft.com/zh-cn/microsoft-edge/visual-studio-code/ide-integration";
var length = FirstRespondingUrlAsync(urlA, urlB);
Console.WriteLine(length);
// 返回第一个响应的URL的数据长度。
static async Task<int> FirstRespondingUrlAsync(string urlA, string urlB)
{
    var httpClient = new HttpClient();

    // 并发地开始两个下载任务。
    Task<byte[]> downloadTaskA = httpClient.GetByteArrayAsync(urlA);
    Task<byte[]> downloadTaskB = httpClient.GetByteArrayAsync(urlB);

    // 等待任意一个任务完成。
    Task<byte[]> completedTask =
      await Task.WhenAny(downloadTaskA, downloadTaskB);

    // 返回从URL得到的数据的长度。
    byte[] data = await completedTask;
    return data.Length;
}