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
using TurningEdge.MakerWow.Api.Models.Relationships;
using TurningEdge.MakerWow.Api.Windows;
using TurningEdge.MakerWow.Models;
using TurningEdge.MakerWow.Models.Relationships;
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

            MakerWOWApi.Initialize<WindowsWebContext>();
            MakerWOWApi.OnError += MakerWOWApi_OnError;
            MakerWOWApi.Login("corey_stjacques@hotmail.com", "9751058aA2",
                onLoginSuccess, OnCrudFailed);
        }

        private static void onLoginSuccess(User user, ApiContext context)
        {
            var relationshipSkillUser1 = new RelationshipSkillUserJsonObject(MakerWOWApi.Id, 1, 0);
            MakerWOWApi.RelationshipSkillUserRepository.Create(relationshipSkillUser1, onCreateSuccessAction, OnCrudFailed);

            var relationshipSkillUser2 = new RelationshipSkillUserJsonObject(MakerWOWApi.Id, 2, 0);
            MakerWOWApi.RelationshipSkillUserRepository.Create(relationshipSkillUser2, onCreateSuccessAction, OnCrudFailed);

            var relationshipSkillUser3 = new RelationshipSkillUserJsonObject(MakerWOWApi.Id, 3, 0);
            MakerWOWApi.RelationshipSkillUserRepository.Create(relationshipSkillUser3, onCreateSuccessAction, OnCrudFailed);
        }

        private static void onCreateSuccessAction(ApiAction context)
        {
            MakerWOWApi.InventoryRepository.Read(onReadSuccessAction, OnCrudFailed);
        }

        private static void onReadSuccessAction(InventoryJsonObject[] worldLayers, ApiResult<InventoryJsonObject> context)
        {
            var stockpile = new StockpileJsonObject(0, MakerWOWApi.Id, 500);
            MakerWOWApi.StockpileRepository.Create(stockpile, onCreateSuccessAction, OnCrudFailed);
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
