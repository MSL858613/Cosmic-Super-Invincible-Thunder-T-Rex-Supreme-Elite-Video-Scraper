using System.Net;
using System.Net.Http.Headers;
using MysteriousThings;

Class1 class1 = new Class1();

while (true)
{
    Console.WriteLine("请输入下载地址");
    string? input = Console.ReadLine();
    if (input is null)
    {
        return;
    }

    if (!Uri.TryCreate(input, UriKind.Absolute, out Uri? downloadUri)
        || (downloadUri.Scheme != Uri.UriSchemeHttp && downloadUri.Scheme != Uri.UriSchemeHttps))
    {
        Console.WriteLine("访问失败，错误码：INVALID_URL");
        continue;
    }

    using var client = new HttpClient { Timeout = TimeSpan.FromSeconds(15) };
    using var request = new HttpRequestMessage(HttpMethod.Get, downloadUri);
    request.Headers.Range = new RangeHeaderValue(0, 0);

    try
    {
        using HttpResponseMessage response = await client.SendAsync(
            request,
            HttpCompletionOption.ResponseHeadersRead);

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"访问失败，错误码：{(int)response.StatusCode} ({response.StatusCode})");
            continue;
        }
    }
    catch (TaskCanceledException)
    {
        Console.WriteLine("访问失败，错误码：TIMEOUT");
        continue;
    }
    catch (HttpRequestException exception)
    {
        int? errorCode = exception.StatusCode is HttpStatusCode statusCode
            ? (int)statusCode
            : (exception.InnerException as System.Net.Sockets.SocketException)?.ErrorCode;
        Console.WriteLine($"访问失败，错误码：{errorCode?.ToString() ?? exception.HResult.ToString()}");
        continue;
    }

    class1.core(input);
    Console.WriteLine("按任意键退出...");
    if (Console.IsInputRedirected)
    {
        Console.ReadLine();
    }
    else
    {
        Console.ReadKey(true);
    }

    break;
}