﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurningEdge.MakerWow.Api.Delegates;
using TurningEdge.MakerWow.Api.Models.Abstracts;
using TurningEdge.MakerWow.Api.Models.Interfaces;

namespace TurningEdge.MakerWow.Api.Interfaces
{
    public interface IApiRepository<T>
        where T : class, IJsonObject
    {
        void Create(T[] models, OnSuccessAction  onCreateSuccessAction, OnFailedAction onCreateFailedAction);
        void Read  (OnGetSuccessAction<T> onReadSuccessAction, OnFailedAction onReadFailedAction, Dictionary<string, string> filter = null);
        void Update(T[] models, OnSuccessAction  onUpdateSuccessAction,  OnFailedAction    onUpdateFailedAction);
        void Delete(T[] models, OnSuccessAction  onDeleteSuccessAction, OnFailedAction onDeleteFailedAction);
    }
}
