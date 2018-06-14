using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TurningEdge.Web.WebContext.Delegates;
using UnityEngine;
using UnityEngine.Networking;

namespace TurningEdge.Web.Unity.Models
{
    public class RequestContainer
    {
        private OnWebRequestFailedAction _onFailed;
        private OnWebRequestSuccessAction _onSuccess;
        private WWWForm _form;

        public OnWebRequestFailedAction OnFailed
        {
            get { return _onFailed; }
            set { _onFailed = value; }
        }
        public OnWebRequestSuccessAction OnSuccess
        {
            get { return _onSuccess; }
            set { _onSuccess = value; }
        }
        public WWWForm Form
        {
            get { return _form; }
            set { _form = value; }
        }
        public RequestContainer(OnWebRequestSuccessAction successAction, 
            OnWebRequestFailedAction failedAction, WWWForm form = null)
        {
            _onSuccess = successAction;
            _onFailed = failedAction;
            _form = form;
        }
    }
}
