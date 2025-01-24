using Microsoft.AspNetCore.Builder;
using Ehsan.CSMS;
using Volo.Abp.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder();
await builder.RunAbpModuleAsync<CSMSWebTestModule>();

public partial class Program
{
}
