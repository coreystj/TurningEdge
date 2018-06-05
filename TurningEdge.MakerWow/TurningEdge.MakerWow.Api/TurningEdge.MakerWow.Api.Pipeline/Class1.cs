using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurningEdge.Debugging;
using TurningEdge.Generics.Factories;
using TurningEdge.MakerWow.Api.Exceptions;
using TurningEdge.MakerWow.Api.Factories;
using TurningEdge.MakerWow.Api.Managers;
using TurningEdge.MakerWow.Api.Models;
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
            MakerWOWApi.Login("corey", "st-jacques", onLoginSuccess, onLoginFailed);

            User user = null;
            WorldLayer worldLayer = null;
            ChunkDataFactory chunkDataFactory = new ChunkDataFactory(user);

            ChunkData[] chunkDatas = new ChunkData[] {
                chunkDataFactory.Create(worldLayer, 0, 0)
            };

            var webContextFactory = new Factory<IWebContext>();
            IWebContext webContext = webContextFactory.Create<WindowsWebContext>();
            MakerWOWApi.SetWebContext(webContext);
            //MakerWOWApi.GetWorldData(OnGetWorldDataSuccess, OnGetWorldDataFailed);
            MakerWOWApi.SetChunkData(chunkDatas, OnGetChunkDataSuccess, OnGetChunkDataFailed);
            Console.ReadLine();
        }

        private static void OnGetChunkDataFailed(ApiException exception)
        {
            throw new NotImplementedException();
        }

        private static void OnGetChunkDataSuccess()
        {
        }
    }
}
