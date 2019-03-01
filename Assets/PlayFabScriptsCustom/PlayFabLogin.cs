using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using System.Collections;
using System.Collections.Generic;

public class PlayFabLogin : MonoBehaviour
    {
        #region Singleton
        public static PlayFabLogin instance;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }
        #endregion

            

        public bool IsDevelopingID = false;

        bool saveLocalInfo = false;

        public Text DebugText;
        public GameObject LogInWindow;

        TextsScript LoginText;

        TextsScript errorLogIn;

        public bool isPlayFabLogged = false;

        bool login = false;

        TextScriptErrors errorText;

        CharacterInfo CI;
        MainMenuAnimator MMA;
        EnvironmentController EC;



        public void Start()
        {
            CI = CharacterReferences.instance.playerInfo;
            MMA = MainMenuAnimator.instance;
            EC = EnvironmentController.instance;
            //StartCoroutine("CheckDataCoroutine");
            //Note: Setting title Id here can be skipped if you have set the value in Editor Extensions already.
            if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
            {
                PlayFabSettings.TitleId = "C3D0"; // Please change this value to your own titleId from PlayFab Game Manager
            }

        }

        public void GetVIV(CharacterInfo ci, MainMenuAnimator mma, EnvironmentController ec, Text debugtext, GameObject loginwindow, TextsScript loginText)
        {
            //Debug
            //Debug.Log("Updating variables Playfab");


            CI = ci;
            MMA = mma;
            EC = ec;
            DebugText = debugtext;
            LogInWindow = loginwindow;
            LoginText = loginText;
            //StartCoroutine("CheckDataCoroutine");
        }

        public void SetErrorText(TextScriptErrors tse)
        {
            errorText = tse;
        }
        public void StartNoInternet()
        {
            saveLocalInfo = true;

            EnvironmentController ec = EnvironmentController.instance;

            Destroy(ec.prefabsInstantiated[0].gameObject);
            ec.setsList = new List<EnvironmentSet>();

            ec.GetEnvironments();
            ec.SetEnvironment();

            ParallaxMainController.instance.SetParallaxNewElements();
			//Debug.Log("PlayFabLogin");

			MMA.UpdateTexts(true);

            MMA.ToogleShopWindow();
            ShopController.instance.InstantiateNewCharacter();
            MMA.ToogleShopWindow();

        }
        public void StartLogIn()
        {
            if (CharacterReferences.instance.playerInfo.firstConection == true)
            {
                LogInWindow.SetActive(true);


                if (IsDevelopingID == true)
                {
                    LogInCustomPlayFab();
                }
            }
            else
            {
                LogInPlayFabDeviceID();
            }

            if (IsDevelopingID == true)
            {
                LogInCustomPlayFab();
            }
            else
            {
                LogInPlayFabDeviceID();
            }
        }

        #region Register User
        public void RegisterUserPlayFab()
        {
            var request = new RegisterPlayFabUserRequest { Username = PlayerPrefs.GetString("Username"), Password = PlayerPrefs.GetString("Password"), RequireBothUsernameAndEmail = false };
            PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnRegisterFailure);
        }
        public void RegisterUserPlayFab(string email)
        {
            var request = new RegisterPlayFabUserRequest { Email = email, Username = PlayerPrefs.GetString("Username"), Password = PlayerPrefs.GetString("Password"), RequireBothUsernameAndEmail = false };
            PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnRegisterFailure);
        }
        private void OnRegisterSuccess(RegisterPlayFabUserResult result)
        {
            Debug.Log("PlayFab - Register Successful");
            DebugText.text += "\nPlayFab - Register Successful";
            if (CI.firstConection || CI.isLocal)
            {
                saveLocalInfo = true;
            }
            if (CI.firstConection == false)
            {
                saveLocalInfo = false;
                CI.ResetLocalData();
            }
            UploadUserData(true);
            LogInPlayFabUsername();
        }

        private void OnRegisterFailure(PlayFabError error)
        {
            Debug.LogWarning("PlayFab - Register Failed");
            Debug.LogError("Here's some debug information:");
            Debug.LogError(error.GenerateErrorReport());
            DebugText.text += "\nPlayFab - Register Failed || " + error;
            /*switch (error.HttpCode)
            {
                case 1009:
                    //UsernameNotAvailable
                    errorText.GetErrorKey(0);
                    break;
                case 1008:
                    //InvalidPassword
                    errorText.GetErrorKey(1);
                    break;
                case 1007:
                    //InvalidUsername
                    errorText.GetErrorKey(2);
                    break;
                default:
                    break;
            }*/
            switch (error.Error)
            {
                case PlayFabErrorCode.UsernameNotAvailable:
                    errorText.GetErrorKey(0);
                    break;
                case PlayFabErrorCode.InvalidParams:
                    errorText.GetErrorKey(4);
                    break;
                case PlayFabErrorCode.InvalidPassword:
                    errorText.GetErrorKey(1);
                    break;
                case PlayFabErrorCode.InvalidUsername:
                    errorText.GetErrorKey(2);
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region Link DeviceID
        public void LinkPlayFabDeviceID()
        {
            var request = new LinkAndroidDeviceIDRequest { AndroidDeviceId = SystemInfo.deviceUniqueIdentifier };
            PlayFabClientAPI.LinkAndroidDeviceID(request, OnLinkDeviceIDSuccess, OnLinkDeviceIDFailure);
        }

        private void OnLinkDeviceIDSuccess(LinkAndroidDeviceIDResult result)
        {
            Debug.Log("PlayFab - Linked Android ID Successful");
            DebugText.text += "\nPlayFab - Linked Android ID Successful";
            if (CI.isLocal)
            {
                UploadUserData(true);
            }
            else
            {
                GetPlayFabData();
            }
            LogInWindow.SetActive(false);
            CI.isLocal = false;
        }

        private void OnLinkDeviceIDFailure(PlayFabError error)
        {
            Debug.LogWarning("PlayFab - LinkAndroidID Failed");
            Debug.LogError("Here's some debug information:");
            Debug.LogError(error.GenerateErrorReport());
            DebugText.text += "\nPlayFab - LinkAndroidID Failed || " + error;
            CI.isLocal = true;
        }
        #endregion

        #region LogIn Google/Apple
        public void LogInPlayFabOS()
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                GooglePlayLogin.instance.StartLogInGoogle();
            }
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                GameCenterLogin.instance.LogInIOS();
            }
            else
            {
                LogInWindow.SetActive(true);
            }
            Debug.Log("PlayFab - LogInOS");
        }

        
        #endregion

        #region LogIn DeviceID
        public void LogInPlayFabDeviceID()
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                var request = new LoginWithAndroidDeviceIDRequest { AndroidDeviceId = SystemInfo.deviceUniqueIdentifier };
                PlayFabClientAPI.LoginWithAndroidDeviceID(request, OnLogInDeviceIDSuccess, OnLogInDeviceIDFailure);
            }
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                var request = new LoginWithIOSDeviceIDRequest { DeviceId = SystemInfo.deviceUniqueIdentifier };
                PlayFabClientAPI.LoginWithIOSDeviceID(request, OnLogInDeviceIDSuccess, OnLogInDeviceIDFailure);
            }
            else if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                //This is Unity Editor so log in Bitch!!!
                DebugText.text += "\nThis is Unity Device";
                LogInWindow.SetActive(true);
            }
        }

        private void OnLogInDeviceIDSuccess(LoginResult result)
        {
            //Debug
            Debug.Log("PlayFab - LogIn Device Successful");
            DebugText.text += "\nPlayFab - LogIn Device Successful";

            //If is first connection to device
            if (CI.firstConection == true)
            {
                //Get Data from user
                GetPlayFabData();
                CI.firstConection = false;
            }
            else
            {
                //Upload User Data for no internet progress
                UploadUserData(true);
            }
        }

        private void OnLogInDeviceIDFailure(PlayFabError error)
        {
            //Debug
            Debug.LogWarning("PlayFab - LogIn Device Failed");
            Debug.LogError("Here's some debug information:");
            Debug.LogError(error.GenerateErrorReport());
            DebugText.text += "\nPlayFab - LogIn Device Failed || " + error;

            //Still first connection
            CI.firstConection = true;

            //Log in window activate to other log in
            LogInWindow.SetActive(true);
        }

        #endregion

        #region LogIn Custom
        public void LogInCustomPlayFab()
        {
            var request = new LoginWithCustomIDRequest { CreateAccount = true, CustomId = PlayerPrefs.GetString("CustomID") };
            PlayFabClientAPI.LoginWithCustomID(request, OnLoginCustomSuccess, OnLoginCustomFailure);
        }
        private void OnLoginCustomSuccess(LoginResult result)
        {
            //Debug
            Debug.Log("PlayFab - Login Custom Successful");
            DebugText.text += "\nPlayFab - Login Custom Successful";

            //If is first connection to this device
            if (result.NewlyCreated)
            {
                //Upload User Data to new user to set variables to Default
                CI.ResetLocalData();
                UploadUserData(true);
                CI.firstConection = false;
            }
            else if(CI.firstConection == true)
            {
                CI.ResetLocalData();
                GetPlayFabData();
                CI.firstConection = false;
            }
            else
            {
                UploadUserData(true);
                //GetCatalogitemsPlayFab();
            }
                //Get Data from server
                isPlayFabLogged = true;
        }

        private void OnLoginCustomFailure(PlayFabError error)
        {
            //Debug
            Debug.LogWarning("PlayFab - Login Custom Failed");
            Debug.LogError("Here's some debug information:");
            Debug.LogError(error.GenerateErrorReport());
            DebugText.text += "\nPlayFab - Login Custom Failed || " + error;

            //Start Default Log In path
            StartLogIn();
        }
        #endregion

        #region LogIn Email
        public void LogInPlayFabEmail()
        {
            var request = new LoginWithEmailAddressRequest { Email = PlayerPrefs.GetString("Email"), Password = PlayerPrefs.GetString("Password") };
            PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginEmailSuccess, OnLoginEmailFailure);
        }
        private void OnLoginEmailSuccess(LoginResult result)
        {
            Debug.Log("PlayFab - Login Email Successful");
            DebugText.text = "PlayFab - Login Email Successful";
            if (CharacterReferences.instance.playerInfo.firstConection == true)
            {
                LogInWindow.SetActive(false);
                CharacterReferences.instance.playerInfo.firstConection = false;
                UploadUserData(true);
            }
            GetPlayFabData();
        }

        private void OnLoginEmailFailure(PlayFabError error)
        {
            Debug.LogWarning("PlayFab - Login Email Failed");
            Debug.LogError("Here's some debug information:");
            Debug.LogError(error.GenerateErrorReport());
            DebugText.text = "PlayFab - Login Email Failed || " + error;
        }
        #endregion

        #region LogIn Username
        public void LogInPlayFabUsername()
        {
            var request = new LoginWithPlayFabRequest { Username = PlayerPrefs.GetString("Username"), Password = PlayerPrefs.GetString("Password") };
            PlayFabClientAPI.LoginWithPlayFab(request, OnLogInUsernameSuccess, OnLoginUsernameFailure);
        }

        private void OnLogInUsernameSuccess(LoginResult result)
        {
            //Debug.Log("PlayFab - Login Username Successful");
            DebugText.text += "\nPlayFab - Login Username Successful";
            if (CI.firstConection == true)
            {
                CI.firstConection = false;
                LinkPlayFabDeviceID();
            }
            if (saveLocalInfo)
            {
                UploadUserData(true);
            }
            else
            {
                GetPlayFabData();
            }
            LogInWindow.SetActive(false);
            //MMA.UpdateTexts();
            CI.isLocal = false;
        }

        private void OnLoginUsernameFailure(PlayFabError error)
        {
            CI.isLocal = true;
            //Debug.LogWarning("PlayFab - Login Username Failed");
            //Debug.LogError("Here's some debug information:");
            //Debug.LogError(error.GenerateErrorReport());
            DebugText.text += "\nPlayFab - Login Username Failed || " + error;
            Debug.Log("Error: " + error.Error);
            /*switch (error.HttpCode)
            {
                case 1001:
                    //Account not found
                    errorText.GetErrorKey(3);
                    break;
                case 1142:
                    //Invalid username or password
                    errorText.GetErrorKey(4);
                    break;
                default:
                    break;
            }*/
            switch (error.Error)
            {
                case PlayFabErrorCode.InvalidParams:
                    errorText.GetErrorKey(4);
                    break;
                case PlayFabErrorCode.AccountNotFound:
                    errorText.GetErrorKey(3);
                    break;
                case PlayFabErrorCode.InvalidUsernameOrPassword:
                    errorText.GetErrorKey(4);
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region Upload Data
        public void UploadUserData(bool isLogin)
        {
            login = isLogin;
            UploadUD1();
        }

        public void UploadUD1()
        {
            if (PlayFabAuthenticationAPI.IsEntityLoggedIn())
            {
                //Debug.Log("PlayFab - UpdateData");

                Dictionary<string, string> data = new Dictionary<string, string>();
                data = CharacterReferences.instance.playerInfo.GetData1();

                var request = new UpdateUserDataRequest { Data = data };
                PlayFabClientAPI.UpdateUserData(request, OnUpdateUserDataSuccess1, OnUpdateUserDataFailure);
            }
            else
            {
                LogInWindow.SetActive(true);
            }
        }
        public void UploadUD2()
        {
            if (PlayFabAuthenticationAPI.IsEntityLoggedIn())
            {
                //Debug.Log("PlayFab - UpdateData");

                Dictionary<string, string> data = new Dictionary<string, string>();
                data = CharacterReferences.instance.playerInfo.GetData2();

                var request = new UpdateUserDataRequest { Data = data };
                PlayFabClientAPI.UpdateUserData(request, OnUpdateUserDataSuccess2, OnUpdateUserDataFailure);
            }
            else
            {
                LogInWindow.SetActive(true);
            }
        }

        private void OnUpdateUserDataSuccess1(UpdateUserDataResult result)
        {
            //Debug.Log("PlayFab - UpdatedDataSuccess 1");
            DebugText.text += "\nPlayFab - UpdatedDataSuccess 1";
            UploadUD2();
        }

        private void OnUpdateUserDataSuccess2(UpdateUserDataResult result)
        {
            //Debug.Log("PlayFab - UpdatedDataSuccess 2");
            DebugText.text += "\nPlayFab - UpdatedDataSuccess 2";
            if (login)
            {
                GetPlayFabData();
            }
            
        }
        private void OnUpdateUserDataFailure(PlayFabError error)
        {
            Debug.Log("PlayFab - UpdatedDataERROR");
            Debug.Log(error);
            DebugText.text += "\nPlayFab - UpdatedDataError";
            DebugText.text += "\n" + error;
        }
        #endregion

        #region Get Data
        public void GetPlayFabData()
        {
            var request = new GetUserDataRequest { };
            PlayFabClientAPI.GetUserData(request, OnGetDataSuccess, OnGetDataFailure);
        }

        private void OnGetDataSuccess(GetUserDataResult result)
        {
            //Debug
            //Debug.Log("PlayFab - GetDataSuccess");
            DebugText.text += "\nPlayFab - GetDataSuccess";

            //Set data from server to Scriptable
            CharacterReferences.instance.playerInfo.SetData(result.Data);

            //Get catalog of items from server
            GetCatalogitemsPlayFab();
        }

        private void OnGetDataFailure(PlayFabError error)
        {
            //Debug.Log("PlayFab - GetDataERROR");
            //Debug.Log(error);
            DebugText.text += "\nPlayFab - GetDataError";
            DebugText.text += "\n" + error;
        }
        #endregion

        #region Get Catalog Items
        public void GetCatalogitemsPlayFab()
        {
            var request = new GetCatalogItemsRequest { };
            PlayFabClientAPI.GetCatalogItems(request, OnGetCatalogItemSuccess, OnGetCatalogItemFailure);
        }

        private void OnGetCatalogItemSuccess(GetCatalogItemsResult result)
        {
            // CatalogItem info: https://api.playfab.com/documentation/Server/datatype/PlayFab.Server.Models/PlayFab.Server.Models.CatalogItem

            //Debug
            //Debug.Log("PlayFab - GetCatalogItemSuccess");
            DebugText.text += "\nPlayFab - GetCatalogItemSuccess";

            //Set Catalog of items to the shop list Scriptable
            CharacterReferences.instance.charactersInfo.GetItemList(result.Catalog);

            //Get User Inventory from Server
            GetPlayFabInventory();
        }

        private void OnGetCatalogItemFailure(PlayFabError error)
        {
            //Debug
            //Debug.Log("PlayFab - GetCatalogItemERROR");
            //Debug.Log(error);
            DebugText.text += "\nPlayFab - GetCatalogItemError";
            DebugText.text += "\n" + error;
        }
        #endregion

        #region Get Inventory
        public void GetPlayFabInventory()
        {
            var request = new GetUserInventoryRequest { };
            PlayFabClientAPI.GetUserInventory(request, OnGetInventorySuccess, OnGetInventoryFailure);
        }

        private void OnGetInventorySuccess(GetUserInventoryResult result)
        {
            //Debug
            //Debug.Log("PlayFab - GetInventorySuccess");
            DebugText.text += "\nPlayFab - GetInventorySuccess";

            //Set Virtual currecy & Items owned
            CharacterReferences.instance.playerInfo.SetCurrency(result.VirtualCurrency["CO"], result.VirtualCurrency["GE"]);
            CharacterReferences.instance.charactersInfo.CheckCharacters(result.Inventory);


            EnvironmentController ec = EnvironmentController.instance;

            ec.setsList = new List<EnvironmentSet>();

            EnvironmentPrefabController temp = ec.prefabsInstantiated[0];
            ec.prefabsInstantiated = new List<EnvironmentPrefabController>();
            Destroy(temp.gameObject);

            ec.GetEnvironments();
            ec.SetEnvironment();

            ParallaxMainController.instance.SetParallaxNewElements();
			//Debug.Log("PlayFabLogin GET INVENTORY");

			MMA.UpdateTexts(true);

            MMA.ToogleShopWindow();
            ShopController.instance.InstantiateNewCharacter();
            MMA.ToogleShopWindow();

            isPlayFabLogged = true;
        }

        private void OnGetInventoryFailure(PlayFabError error)
        {
            //Debug
            //Debug.Log("PlayFab - GetInventoryERROR");
            //Debug.Log(error);
            DebugText.text += "\nPlayFab - GetInventoryError";
            DebugText.text += "\n" + error;
        }
        #endregion

        #region Purchase Item
        public void PurchaseItemPlayFab(int itemID, string currency, int price)
        {
            var request = new PurchaseItemRequest { ItemId = itemID.ToString(), VirtualCurrency = currency, Price = price };
            PlayFabClientAPI.PurchaseItem(request, OnPurchaseItemSuccess, OnPurchaseItemFailure);
        }

        private void OnPurchaseItemSuccess(PurchaseItemResult result)
        {
            Debug.Log("PlayFab - PurchaseItemSuccess");
            MainMenuAnimator.instance.shopPopup.SetActive(false);
            DebugText.text += "\nPlayFab - PurchaseItemSuccess";
            GetPlayFabInventory();
        }

        private void OnPurchaseItemFailure(PlayFabError error)
        {
            Debug.Log("PlayFab - PurchaseItemERROR");
            Debug.Log(error);
            DebugText.text += "\nPlayFab - PurchaseItemError";
            DebugText.text += "\n" + error;
            switch (error.Error)
            {
                case PlayFabErrorCode.InsufficientFunds:
                    //InsufficientFunds
                    errorText.GetErrorKey(5);
                    break;
                default:
                    break;
            }
        }
        #endregion

        #region AddVirtualCurrency
        public void AddPlayFabVirtualCurrecy(int i, string virtualCurrecy)
        {
            var request = new AddUserVirtualCurrencyRequest { Amount = i, VirtualCurrency = virtualCurrecy };
            PlayFabClientAPI.AddUserVirtualCurrency(request, OnAddVirtualCurrencySuccess, OnAddVirtualCurrencyFailure);
        }

        private void OnAddVirtualCurrencySuccess(ModifyUserVirtualCurrencyResult result)
        {
            //Debug.Log("Playfab - AddVirtualCurrency Success_Balance: " + result.Balance);
            DebugText.text += "\nPlayfab - AddVirtualCurrency Success_Balance: " + result.Balance;
            //GetPlayFabInventory();
        }
        private void OnAddVirtualCurrencyFailure(PlayFabError error)
        {
            Debug.Log("PlayFab - AddVirtualCurrency ERROR");
            Debug.Log(error);
            DebugText.text += "\nPlayFab - AddVirtualCurrency Error";
            DebugText.text += "\n" + error;
        }
        #endregion

        IEnumerator CheckDataCoroutine()
        {
            while (EC.inGame == false)
            {
                yield return new WaitForSeconds(30f);
                UploadUserData(false);
            Debug.Log("Periodical update PlayFab");
            }
            StopCoroutine("CheckDataCoroutine");
        }
    }


