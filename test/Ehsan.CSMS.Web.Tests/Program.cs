using Ehsan.CSMS;
using Microsoft.AspNetCore.Builder;
using Volo.Abp.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder();
await builder.RunAbpModuleAsync<CSMSWebTestModule>();

public partial class Program
{
}
