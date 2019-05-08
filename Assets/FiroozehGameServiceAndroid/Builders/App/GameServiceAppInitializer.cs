using System;
using FiroozehGameServiceAndroid.Core;
using FiroozehGameServiceAndroid.Core.App;

namespace FiroozehGameServiceAndroid.Builders.App
{
    internal static class GameServiceAppInitializer
    {
        internal static void Init(
            GameServiceClientConfiguration configuration
            ,Action<GameServiceApp> onSuccess
            ,Action<string> onError)
        {

                FiroozehGameServiceAppLoginCheck.CheckUserLoginStatus(
                    configuration.CheckAppStatus
                    , configuration.CheckOptionalUpdate
                    , isLogin =>
                    {
                        if (isLogin)
                        {
                            AppPluginHandler.InitGameService(
                                configuration.ClientId,configuration.ClientSecret,
                                s => { onSuccess.Invoke(new GameServiceApp(s, configuration.HaveNotification)); }
                                ,onError.Invoke);
                        }      
                        else
                            FiroozehGameServiceAppLoginCheck.ShowLoginUI(
                                configuration.CheckAppStatus
                                , configuration.CheckOptionalUpdate
                                , r =>
                                {
                                    if (r)
                                    {
                                        AppPluginHandler.InitGameService(
                                            configuration.ClientId, configuration.ClientSecret,
                                            s =>
                                            {
                                                onSuccess.Invoke(new GameServiceApp(s, configuration.HaveNotification));
                                            }
                                            , onError.Invoke);
                                    }
                                }, onError.Invoke);
                    }, onError.Invoke);
            
        }
    }
}