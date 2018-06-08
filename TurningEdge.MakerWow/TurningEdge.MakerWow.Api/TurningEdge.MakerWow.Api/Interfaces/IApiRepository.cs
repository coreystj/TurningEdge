using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurningEdge.MakerWow.Api.Delegates;
using TurningEdge.MakerWow.Api.Models.Abstracts;

namespace TurningEdge.MakerWow.Api.Interfaces
{
    public interface IApiRepository<T>
        where T : JsonObject
    {
        void Create(T[] models, OnSuccessAction  onCreateSuccessAction, OnFailedAction onCreateFailedAction);
        void Read  (OnGetSuccessAction<T> onReadSuccessAction, OnFailedAction onReadFailedAction);
        void Update(T[] models, OnSuccessAction  onUpdateSuccessAction,  OnFailedAction    onUpdateFailedAction);
        void Delete(T[] models, OnSuccessAction  onDeleteSuccessAction, OnFailedAction onDeleteFailedAction);
    }
}
