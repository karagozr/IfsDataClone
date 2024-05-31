using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IfsDataClone.Services;

public class IFSDataServiceFilter
{
    public string CompanyId { get; set; }
    public DateTime? Date1 { get; set; }
    public DateTime? Date2 { get; set; }
    public int Size { get; set; }
    public int Skip { get; set; }

}

public class IFSDataService
{

    public async Task<ICollection<dynamic>> List(IFSDataServiceFilter filter)
    {

        HttpClient client = new ();
        var request = new HttpRequestMessage(HttpMethod.Get,
            $"https://erp.ozyer.com/main/ifsapplications/projection/v1/SalesObjectsHandling.svc/SalesObjectSet?$filter=(Company%20eq%20%2701029-3%27)&$select=SaftCategory,ProjectActivityId,ValidationDate,RequiredString,Company,Lu,ObjectId,Description,Price,PriceType,DelivTypeId,Taxable,TaxCode,TaxClassId,UnitOfMeasure,CodeA,Objgrants,CodeB,CodeC,CodeD,CodeE,CodeF,CodeG,CodeH,CodeI,CodeJ,ProcessCode,Quantity,Text,ProjectId,TaxIdNumber,VoucherText,HsnSacCode&$skip=0&$top=961");
        request.Headers.Add("Authorization", "Bearer " + IFSAuthData.AccessToken);
        
        var response = await client.SendAsync(request);
        if (response.StatusCode != System.Net.HttpStatusCode.OK)
            return new List<dynamic>();

        string responseContent = await response.Content.ReadAsStringAsync();
        var data = JObject.Parse(responseContent).Values().ToArray()[1].ToArray();

        return data;
    }
}
