// See https://aka.ms/new-console-template for more information
using System.Text.Json;
using System.Threading.Tasks;

Console.WriteLine("Hello, World!");

// bao KPI Nhom TKDA
List<string> listBaoCao = [];

// sep Khang
Console.WriteLine($"Main bat dau");

// async bat dong bo,  chi Khang keu 3 dua bat dau lam bao cao
Task<string> taskGiang = GiangTaskAsync();
Task<string> taskPhong = PhongTaskAsync();
Task<string> taskViet = VietTaskAsync();

// sync
// string taskGiang = await GiangTaskAsync();
// string taskPhong = await PhongTaskAsync();
// string taskViet = await VietTaskAsync();

// _ = await Task.Run(() => DOSOmThi());

// Khang lam bao cao cua chị
Console.WriteLine($"Khang bat dau");
listBaoCao.Add("Khang Bao Cao");
Console.WriteLine($"Khang ket thuc");

// listBaoCao.Add(taskGiang);
// listBaoCao.Add(taskPhong);
// listBaoCao.Add(taskViet);

// chị làm xong rồi, nên chờ cả 3 đứa kia làm xong
await Task.WhenAll(taskPhong, taskViet, taskGiang);

// chi Khang tong hop
listBaoCao.Add(taskPhong.Result);
listBaoCao.Add(taskViet.Result);
listBaoCao.Add(taskGiang.Result);

// Gui bao cao
Console.WriteLine($"Main KQ: {JsonSerializer.Serialize(listBaoCao)}");

Console.WriteLine($"Main ket thuc");

await RunWithCancellationAsync();


async Task<string> GiangTaskAsync()
{
    Console.WriteLine($"{nameof(GiangTaskAsync)} bat dau");
    await Task.Delay(3000);
    Console.WriteLine($"{nameof(GiangTaskAsync)} ket thuc");
    return "Giang bao cao";
}

async Task<string> PhongTaskAsync()
{
    Console.WriteLine($"{nameof(PhongTaskAsync)} bat dau");
    await Task.Delay(2500);
    Console.WriteLine($"{nameof(PhongTaskAsync)} ket thuc");
    return "Phong bao cao";
}

async Task<string> VietTaskAsync()
{
    Console.WriteLine($"{nameof(VietTaskAsync)} bat dau");
    await Task.Delay(2000);
    Console.WriteLine($"{nameof(VietTaskAsync)} ket thuc");
    return "Viet bao cao";
}

async Task DoWorkAsync(string name, int delayMs, CancellationToken? token = null)
{
    Console.WriteLine($"{name} started");

    int step = 500;
    for (int i = 0; i < delayMs; i += step)
    {
        Console.WriteLine($"{name} work {i}");
        if(token is not null)
        {
            token.Value.ThrowIfCancellationRequested();
            await Task.Delay(step, token.Value);
        }
    }

    Console.WriteLine($"{name} completed");
}

async Task RunWithCancellationAsync()
{
    using var cts = new CancellationTokenSource();

    CancellationToken token = cts.Token;

    // Optional: auto-cancel after timeout
    // cts.CancelAfter(TimeSpan.FromSeconds(2));

    Task task1 = DoWorkAsync("Task1", 3000, token);
    Task task2 = DoWorkAsync("Task2", 5000, token);
    Task task3 = DoWorkAsync("Task3", 2000);

    var allTasks = Task.WhenAll(task1, task2, task3);

    try
    {
        // Simulate user cancel after 1 second
        _ = Task.Run(async () =>
        {
            await Task.Delay(1000);
            cts.Cancel();
        });

        await allTasks;
    }
    catch (OperationCanceledException)
    {
        Console.WriteLine("Operation was cancelled.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Other error: {ex.Message}");
    }
}

string DOSOmThi(){
    return "Doi so thuc thi";
}