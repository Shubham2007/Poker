using Microsoft.Extensions.DependencyInjection;
using PokerGame.Contracts;
using PokerGame.Core.Cloners;
using PokerGame.Core.Factories;
using PokerGame.Poker;
using PokerGame.Poker.Interfaces;
using System;

namespace PokerGame.Helper
{
    sealed class DependencyInjectionHelper
    {
        private DependencyInjectionHelper() { }

        private static ServiceCollection _serviceCollection = null;

        public static ServiceProvider SetupDI()
        {
            var services = GetServiceCollection();
            ConfigureServices(services);
            return services.BuildServiceProvider();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services
                .AddSingleton<IWinningStrategyFactory, WinningStrategyFactory>()
                .AddTransient<IPokerHandEvaluator, PokerHandEvaluator>()
                .AddTransient<IBet, Bet>()
                .AddTransient<IDealer, Dealer>()
                .AddTransient<IDeck, Deck>()
                .AddTransient(typeof(ICloningStrategy<>), typeof(SerializerCloning<>));
        }

        private static ServiceCollection GetServiceCollection()
        {
            if (_serviceCollection == null)
                _serviceCollection = new ServiceCollection();

            return _serviceCollection;
        }
    }
}
