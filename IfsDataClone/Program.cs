using IfsDataClone.Services;



IFSAuthService ifsAuthService = new();
var status = await ifsAuthService.Login();

if (!status)
{
    Console.WriteLine("Login Hatası");
    return;
}

IFSDataService ifsDataService = new();
var resData = await ifsDataService.List(new IFSDataServiceFilter { Size=1000000,Skip=0});

int i = 1;

foreach (var item in resData.ToList())
{
    Console.WriteLine($"{i++} ------- {item["ObjectId"]}-{item["Description"]}");
}

Console.ReadLine();

