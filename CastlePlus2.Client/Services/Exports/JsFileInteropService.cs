using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace CastlePlus2.Client.Services.Exports;

public sealed class JsFileInteropService : IFileInteropService, IAsyncDisposable
{
    private readonly Lazy<Task<IJSObjectReference>> _moduleTask;

    public JsFileInteropService(IJSRuntime js)
    {
        _moduleTask = new Lazy<Task<IJSObjectReference>>(() =>
            js.InvokeAsync<IJSObjectReference>(
                    "import",
                    "./_content/CastlePlus2.Client/js/cp2-file-interop.js")
               .AsTask());
    }

    public async Task DownloadAsync(string fileName, string contentType, byte[] bytes, CancellationToken ct = default)
    {
        var module = await _moduleTask.Value;
        await module.InvokeVoidAsync("downloadFile", ct, fileName, contentType, bytes);
    }

    public async Task<string> CreateObjectUrlAsync(string contentType, byte[] bytes, CancellationToken ct = default)
    {
        var module = await _moduleTask.Value;
        return await module.InvokeAsync<string>("createObjectUrl", ct, contentType, bytes);
    }

    public async Task RevokeObjectUrlAsync(string objectUrl, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(objectUrl))
            return;

        var module = await _moduleTask.Value;
        await module.InvokeVoidAsync("revokeObjectUrl", ct, objectUrl);
    }

    public async ValueTask DisposeAsync()
    {
        if (_moduleTask.IsValueCreated)
        {
            var module = await _moduleTask.Value;
            await module.DisposeAsync();
        }
    }
}
