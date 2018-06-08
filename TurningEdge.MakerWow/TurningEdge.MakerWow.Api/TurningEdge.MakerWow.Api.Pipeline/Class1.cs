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
using TurningEdge.MakerWow.Api.Models.Abstracts;
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

            MakerWOWApi.OnError += MakerWOWApi_OnError;
            MakerWOWApi.Login("corey_stjacques@hotmail.com", "9751058aA2", 
                onLoginSuccess, OnCrudFailed);
        }

        private static void onLoginSuccess(User user, ApiContext context)
        {
            MakerWOWApi.WorldLayerRepository.Read(
                onGetWorldLayersSuccessAction,
                OnCrudFailed);
        }

        private static void onGetWorldLayersSuccessAction(WorldLayer[] worldLayers, ApiResult<WorldLayer> context)
        {

            WorldLayer worldLayer = worldLayers[0];
            ChunkDataFactory chunkDataFactory = new ChunkDataFactory(MakerWOWApi.CurrentUser);

            ChunkData[] chunkDatas = new ChunkData[] {
                chunkDataFactory.Create(worldLayer, 6, 5),
                chunkDataFactory.Create(worldLayer, 7, 5),
                chunkDataFactory.Create(worldLayer, 8, 5),
                chunkDataFactory.Create(worldLayer, 9, 5)
            };
            MakerWOWApi.ChunkDataRepository.Create(
                chunkDatas, OnSetChunkDataSuccess, OnCrudFailed);
        }

        private static void OnSetChunkDataSuccess(ApiContext context)
        {
            MakerWOWApi.ChunkDataRepository.Read(OnGetWorldDataSuccess, OnCrudFailed);
        }

        private static void OnGetWorldDataSuccess(ChunkData[] chunks, ApiResult<ChunkData> context)
        {

            Console.ReadLine();
        }


        private static void MakerWOWApi_OnError(ApiException exception)
        {
            Debugging.Debugger.PrintError(exception);
        }

        private static void OnCrudFailed(ApiContext context)
        {
            Debugging.Debugger.PrintError(context.Error);
        }
    }
}
