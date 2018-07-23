# ClArc
CleanArchitecture Library.

# 使い方
※ ClArc.Tests プロジェクトの AsyncUseCaseBusTest.cs / SyncUseCaseBusTest.cs を見るのがわかりやすいかと思います。  

リクエスト、レスポンスを定義し、そのハンドラー （Request を受け取り、Response を戻すクラス） を定義します。  

```
public class MyRequest : IRequest<MyResponse> {
}

public class MyResponse : IResponse {
}

public class MyInteractor : IUseCase<MyRequest, MyResponse>{
    public MyResponse Handle(MyRequest request){
        return new MyResponse();
    }
}
```

リクエストに対するハンドラーの登録は以下のように行います。
(ServiceCollection は ASP.NET CORE で利用されている DI の仕組みです)
```
void Register(){
    var serviceCollection = new Microsoft.Extensions.DependencyInjection.ServiceCollection();
    var busBuilder = new SyncUseCaseBusBuilder(serviceCollection);
    busBuilder.RegisterUseCase<MyRequest, MyInteractor>();
}

```

あとは UseCaseBus の Handle メソッドに Request を渡すだけで Interactor が実行されて Response を受け取ることができます。
```
class MyController : Contoller{
    private readonly UseCaseBus bus;

    public MyController(UseCaseBus bus){
        this.bus = bus;
    }

    public void Test(){
        var request = new MyRequest();
        var response = bus.Handler(request);
    }
}
```

# How To Use
Note: I think that it is easy to see the AsyncUseCaseBusTest.cs / SyncUseCaseBusTest.cs of the ClArc.Tests project.  

Define request, response and the handler. The handler receive the request and return the response.
```
public class MyRequest : IRequest<MyResponse> {
}

public class MyResponse : IResponse {
}

public class MyInteractor : IUseCase<MyRequest, MyResponse>{
    public MyResponse Handle(MyRequest request){
        return new MyResponse();
    }
}
```

Register the handler for the request as follows.
(ServiceCollection is the mechanism of DI used in ASP.NET CORE)
```
void Register(){
    var serviceCollection = new Microsoft.Extensions.DependencyInjection.ServiceCollection();
    var busBuilder = new SyncUseCaseBusBuilder(serviceCollection);
    busBuilder.RegisterUseCase<MyRequest, MyInteractor>();
}

```

All you need to do is pass the Request to the Handle method of UseCaseBus and the Interactor will be executed to receive the Response.
```
class MyController : Contoller{
    private readonly UseCaseBus bus;

    public MyController(UseCaseBus bus){
        this.bus = bus;
    }

    public void Test(){
        var request = new MyRequest();
        var response = bus.Handler(request);
    }
}
```