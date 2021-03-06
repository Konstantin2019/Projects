using CommonServiceLocator;
using FireBase_lib.Services;
using GalaSoft.MvvmLight.Ioc;
using FireChat.ViewModel.WPFServices;
using System;

namespace FireChat.ViewModel
{
    /// <summary>
    /// ����� ��� ����������� ��������
    /// </summary>
    public class ViewModelLocator
    {
        private static FireChatViewModel singleInstance;

        public ViewModelLocator()
        {
            var service = SimpleIoc.Default;
            ServiceLocator.SetLocatorProvider(() => service);

            service.Register<FireChatViewModel>();

            service.TryRegister<MessangerActions>()
                   .TryRegister<WPFDialogService>()
                   .TryRegister<WindowsManager>();
        }

        public FireChatViewModel FireChatVM
        {
            get
            {
                if (singleInstance != null)
                    return singleInstance;
                else
                {
                    singleInstance = ServiceLocator.Current.GetInstance<FireChatViewModel>();
                    return singleInstance;
                }
            }
        }
    }

    public static class SimpleIocExtention
    {
        public static SimpleIoc TryRegister<TInterface, TService>(this SimpleIoc services)
            where TInterface : class
            where TService : class, TInterface
        {
            if (services.IsRegistered<TInterface>()) return services;
            services.Register<TInterface, TService>();
            return services;
        }

        public static SimpleIoc TryRegister<TService>(this SimpleIoc services)
            where TService : class
        {
            if (services.IsRegistered<TService>()) return services;
            services.Register<TService>();
            return services;
        }

        public static SimpleIoc TryRegister<TService>(this SimpleIoc services, Func<TService> Factory)
            where TService : class
        {
            if (services.IsRegistered<TService>()) return services;
            services.Register(Factory);
            return services;
        }
    }
}