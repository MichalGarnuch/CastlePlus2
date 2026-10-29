using System;
using CastlePlus2.Infrastructure.Services.Auth;
var service = new PasswordHashService();
void Check(bool ok) { if (!ok) throw new Exception("Password contract failed"); }
var hash = service.Hash("zażółć🔐");
Check(service.Verify("zażółć🔐", hash)); Check(!service.Verify("wrong", hash));
foreach (var invalid in new[]{ "", "v1|0|a|b", "v1|2000000000|a|b", "v1|1|!!|!!", "v1||1|a|b", "v2|1|a|b" }) Check(!service.Verify("x",invalid));
Console.WriteLine("Password hash contracts passed");
