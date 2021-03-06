﻿namespace GoatJira.ViewModel
{
    using GalaSoft.MvvmLight;
    using GoatJira.Helpers;
    using GoatJira.Model.LoginInformation;

    class LoginInformationViewModel : ViewModelBase
    {
        private readonly ILoginInformationModelService loginInformationModelService;
        private readonly IDialogService dialogService;

        public LoginInformationModel LoginInformation { get; private set; }

        /// <summary>
        /// Is set to true when data is read.
        /// </summary>
        public bool WasDataRead { get; set; } = false;


        public LoginInformationViewModel(): this (
            IsInDesignModeStatic ? (ILoginInformationModelService)new Design.DesignLoginInformationModelService() : new LoginInformationModelService(), 
            new DialogService())
        {
#if DEBUG
            if (IsInDesignMode)
                LoginInformation = loginInformationModelService.Read(null);
#endif
        }

        public LoginInformationViewModel(ILoginInformationModelService LoginInformationModelService, IDialogService DialogService)
        {
            loginInformationModelService = LoginInformationModelService;
            dialogService = DialogService;
        }

        public void ReadData(EA.Repository Repository)
        {
            LoginInformation = loginInformationModelService.Read(Repository.ProjectGUID);
            WasDataRead = true; 
        }

        public void SaveData(EA.Repository Repository) =>
            loginInformationModelService.Save(LoginInformation, Repository.ProjectGUID);
    }
}
