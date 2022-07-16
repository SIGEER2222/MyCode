static async Task<int> DelayAndReturnAsync(int val)
{
    await Task.Delay(TimeSpan.FromSeconds(val));
    return val;
}

// 当前，此方法输出“2”,“3”,“1”。
// 我们希望它输出“1”,“2”,“3”。
static async Task ProcessTasksAsync()
{
    // 创建任务队列。
    Task<int> taskA = DelayAndReturnAsync(2);
    Task<int> taskB = DelayAndReturnAsync(3);
    Task<int> taskC = DelayAndReturnAsync(1);
    var tasks = new[] { taskA, taskB, taskC };

    // 按顺序await每个任务。
    foreach (var task in tasks)
    {
        var result = await task;
        Trace.WriteLine(result);
    }
}


