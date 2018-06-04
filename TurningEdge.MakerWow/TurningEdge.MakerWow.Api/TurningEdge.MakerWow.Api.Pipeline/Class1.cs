using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurningEdge.Debugging;
using TurningEdge.Generics.Factories;
using TurningEdge.MakerWow.Api.Exceptions;
using TurningEdge.MakerWow.Api.Managers;
using TurningEdge.MakerWow.Api.Models.GameInstances;
using TurningEdge.MakerWow.Api.Windows;
using TurningEdge.Web.Exceptions;
using TurningEdge.Web.WebContext.Interfaces;
using TurningEdge.Web.WebResult.Interfaces;
using TurningEdge.Web.Windows.WebContext.Concretes;

namespace TurningEdge.MakerWow.Api.Pipeline
{
    public static class Class1
    {
        private static void Main(string[] args)
        {
            var webContextFactory = new Factory<IWebContext>();
            IWebContext webContext = webContextFactory.Create<WindowsWebContext>();
            MakerWOWApi.SetWebContext(webContext);
            MakerWOWApi.GetWorldData(OnGetWorldDataSuccess, OnGetWorldDataFailed);
            Console.ReadLine();
        }

        private static void OnGetWorldDataFailed(ApiException exception)
        {
            throw new NotImplementedException();
        }

        private static void OnGetWorldDataSuccess(WorldData[] worldData)
        {
            Debugger.Print(worldData.Length);
        }
    }
}
