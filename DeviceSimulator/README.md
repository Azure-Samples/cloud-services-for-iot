# Connecting the Simulator
Before connecting a physical device to the Cloud Services for IoT, it is suggested that a simulator project be used to test connectivity, model data payloads, and create initial report/dashboard templates. This technical guide will help Cloud Service Providers (CSP) configure the simulator provided in the accompanying source code. Once configured, it will push simulated data into the IoT Hub and other components of the deployed solution.

## Technical Guide
This technical guide will show you how to deploy the sensor simulator portion of the Azure IoT-in-a-Box CSP Solution to the CSP Azure Portal. When you've completed this guide, you will have provisioned and configured the following resources:

+	An App Service (previously referred to as Azure Web Apps) dedicated to Azure WebJobs
+	An Azure SQL Database dedicated to the backend of the Azure WebJobs application
+	A WebJob application to support and be the simulated sensor

Before starting this guide, you will need to install a couple of tools on your local PC, this guide assumes you are running an RTM version of Windows 10.  

### Prerequisites

+  Mobile Tools for Windows 10 - [Windows 10 SDK] 
+  Mobile Emulator for Windows 10 - [Windows 10 Mobile Emulator]
+  A provisioned Azure Subscription: This guide is designed for Cloud Service Providers (CSP), but any relevant Azure Subscription will suffice.
     + Provisioned CSP account and customer subscription in portal
     + Administrative access to a CSP Subscription (this assumes you already have a co-admin login for a CSP subscription)
+  Visual Studio 2015 Community Edition or greater (Visual Studio Professional is the version used for this document) 
+  [Azure SDK for Visual Studio 2015] (March 2016 is the version used for this document)


### Version
0.0.1

### Preparing Visual Studio Environment

Open the SensorTagUniveral.sln file.  This will open the SensorTagUniversal Visual Studio project.  If you do not have the emulator and SDK installed, your project will load and require an update, like this:

![VS - Download Update View](https://2hm2va-ch3302.files.1drv.com/y3mXWGACHvsV2n2Vvdv7LkEeR0VEWEP3eGDGMZbIQgW5ly8PinMRePUyBpxwAsYhOc1YNGByLl9VAVReI0FBm8tvG9aZX7JC9WjQyyBmNXMvN4PDQ0W5PSIrlO_KapjRxYIm2E334Em6amoROtlTikHb9PlsoLbRdAF1enwwM-tlus?width=660&height=342&cropmode=none)

From there you can right-click the project in your Solution Explorer and select **Download Update**.

![VS - Right Click Download Update Menu](https://2hmxva-ch3302.files.1drv.com/y3mZv23vgLpB4zJrNwjiAdAnx5e0KaKKjVLx_G3LHkvdfL9TZmWEuAYx7P9dArZ46V5tB7-5vSPX9QLd0T57O1A9Ez-rtRrhTMuegOv7PUk71ShD6Aj7oMz-KqXXkmAc77n0_WdzmqapfkKaJnLtyT4ChaFPzv21vwC1V9Upe47zsg?width=660&height=344&cropmode=none)

You will be taken to a Microsoft download page to download the following items:

+ The [Windows 10 Mobile Emulator]
+ The [Windows 10 SDK]

Once you have downloaded and installed both files, reload the project.  In some of the test cases, the **Reload Project** menu didn't work, so a work around is to close Visual Studio 2015 entirely and re-open it. 

![VS - Reload Project](https://2hmwva-ch3302.files.1drv.com/y3mdEdpKCRKKrVwS52lKRsfOIh_ooa-Dpu3cdxf7BBEmNV0NCZ6uT7JMVmkxoGSeK58uhXYTGs1ZTgZhYkTe8V-_aSj_-BD8C9PapS8Ou565gTr64H3MfeFpFutvki-FW58wxKJf3m1CGFFWd9st-3Oh-F67MprGsKxY-kyc8BDqZE?width=660&height=342&cropmode=none)

Once the project is loaded correctly, your Solution Explorer and Visual Studio environment will contain one solution and two projects. 

![VS - Proper View](https://0xpsma-ch3302.files.1drv.com/y3m0hVTKHLXTow2AYYkpWfJQuFipSg4rHuNPpSXbMvUz55niR8W9mdmvNrKArvv84-T6RFV1mIGNJOXz5LD1BSveyXk6jHYFAldFk_O_izzzboLOJ53tpSt8ZMu7kyELqAGxLdRzaCbqmfz_evzeA3O1ilP03KOjCzJCalysIWQ8P0?width=660&height=326&cropmode=none)

Now you are ready to setup and configure the endpoints the simulator will use to transmit messages. 

### Configuring the simulated field gateway

The project labeled **DeviceToCloudMessages** handles communication with the Cloud Services for IoT hosted in Azure. This project can serve as an example of processing json messages and sending them to Azure. The following configuration steps will configure this application to communicate with the infrastructure deployed in the prior technical guide. 

![DeviceToCloudMessages - App.config](https://0xprma-ch3302.files.1drv.com/y3mA3ytmN4zKxkSq_QqPtVAr2AE_5zoKRc-3S3VqR6EnrVVzKtP8_8QvS5xOL-S2wGEdwQVq5JmTuE_7oAzw2v6RBJsyB1vueSJW24h5tPqb7BZFv_RzA6f-HgqdxJXHWRRz4bKCk7RANxgLYgXdiEXruzMbB2lc9ZsZJtZsSsSwcU?width=660&height=326&cropmode=none)

The **App.config** file in this project contain connection parameters used to send data payloads to Azure. Open the App.config file for the first project **DeviceToCloudMessages**. Change the IoT Hub name and key of the hub with Write permissions to the Hub. Then save the file.

**IMPORTANT NOTE:** These variables should have been saved from the outputs of the prior Technical Guide: Deploying the Azure IoT Infrastructure.


### Configuring the simulated field gateway
The project labeled **SimulatedDevice** is a command line application that will simulate devices by randomly generating device telemetry values. This project can serve as an example generating and modeling data to send to Azure. The following configuration steps will control how the simulated devices self-register with Azure.
The **App.config** file in the **SimulatedDevice** project contains connection parameters used to self-register devices with the Azure IoT Hub. Open the App.config file. Change the IoT Hub name and key of the hub with Write permissions to the Hub. Then save the file.

**IMPORTANT NOTE:** These variables should have been saved from the outputs of the prior Technical Guide: Deploying the Azure IoT Infrastructure.


![SimulatedDevice - App.config](https://0xpmma-ch3302.files.1drv.com/y3mBk6RexaPSNqYA9_n2mEzy2Kdku4tbmvXV59M4GhleXH9ZolIW9S9kTheveoFd7hs7lxL2TO0oc0LcX0aSgMuli75sBJBPgtqk1BMADbIiuNigb0XZVFdkXqdG6TSjuYSZW5FzsoxQeXMwQ-M0zHlFLMNswvxWmLr7L-BDTf_pkE?width=660&height=328&cropmode=none)

## Update NuGet & Build the solution  
This guide assumes you know how to update NuGet package through the Visual Studio Package Manager (see:  https://docs.nuget.org/consume/package-manager-dialog for loading and managing packages in Visual Studio).  Once you have updated the assemblies and references, build the solution which should create the **SimulatedDevice.exe** [Azure WebJob] program.  If you have updated all of your assemblies correctly the output of your build should resemble the following:

![VS - Simulator Build Output](https://0hprma-ch3302.files.1drv.com/y3mN_U7RbjkZVNo5xldWNKmwBiXNMCLjCOPBxVHU7SZ954nBvUk9bbyvs5tHtXJoliswV0a9nJZ46XqMribF7qm7jqlWxwR-ckQwEJcj2_p-m9lkfDvXPptX1hCPoClWk6ClgHp1MpNK5JQacuwU3MQoN2_2ujDqjDyrvo8MfuDDHo?width=660&height=140&cropmode=none)

Technically, you could deploy this executable to a server by FTP or copying the files to the server.  However, Microsoft Azure provides a deployment template and process that makes deploying WebJobs easy and extensible. 

### Deploying Using Visual Studio

Deploying from Visual Studio is very easy to do.  To deploy this as a WebJob to an Azure App Service, right-click the **SimulatedDevice** project, and select **Publish as Azure WebJob...**.  

![Deploy as Azure WebJob](https://0hpmma-ch3302.files.1drv.com/y3mWsXtINqx3qO6Bq_zU32VuqN4uLH-1dyLT39yl-j7AY8HUFbdwKDQdllWZdLt8Hvjl54eL-nYcVlsx1bMXP3hBzz6UECKTMPQ1m8qZ0aSB8X9RxOBL7zk3FFluaP75qqhLxVlYqjpvlRRkfsLHYNKA-RUazyVJl533ivyaFq1G3A?width=192&height=256&cropmode=none)

This will bring up the **Add Azure WebJob Wizard**.  Change the **WebJob run mode:** field to **Run on Demand**.  This job is not designed to run continuously.  Click **OK**.  This will bring up the publishing profile wizard.  For this guide, we will be publishing to a **Microsoft Azure Web Apps** publishing target.  Once you click on that option you will be prompted to connect to the Azure Subscription you are deploying to.  At this step, you will need to deploy to an existing Web App/App Service, or create a new one. 

![Select App Service to Deploy to](https://0hpoma-ch3302.files.1drv.com/y3mXRHRGuoIFhrzHxni7ZHjvdPgyKWk_NxRjn_7w5wTIyTSZtAhc_zpK8fHVPYjT2UInJL8y0Uvdxvx8J9hfhecf4DxOkUj_J3Pcs1AVr7toaJQYsd2nXfLsIElz2Ly-aFiDn4T56RsF3AM5WnXfpiEyeuDbwZB5vPiRkxXilu3jPQ?width=256&height=232&cropmode=none)

Unless you already have an App Service created, you'll want to select the **New...** button.  This will open the **Create Web App on Microsoft Azure** form in the Wizard.  It is critical that the following fields are set appropriately:
+ **Resource Group** - this needs to be the **CSPIoT** resource group where the core solution is deployed
+ **Region** - this should run in the same region that the core solution runs in, the resource group can span multiple regions, so do not get confused
+ **Database Server** - if you're creating a new one, remember to use all lowercase

![Create Web App on Microsoft Azure](https://0hplma-ch3302.files.1drv.com/y3mxxpttvjRGnC3wwTg48C891RGjOv2yf_C5qScEcnxoLDgnNqXY5Kig8A78ldKOJa5PkyJO65JFmbkzdUFcVa1JcV0Jn0iZZSikkwKu7WXhczxCiEQWKirVNgWNY8rF80lljfzPXI0vtIrMyx970yzbPes4_t1JUvsf9jjVoGquYk?width=256&height=245&cropmode=none)

Once this completes, you will see the **Publish Web** UI change to include the Publishing Profile and appear ready to be deployed.  You can click on the **Validate Connection** button.  You do not want to change the password here.  Click **Publish**

![Validate before publishing](https://0hpnma-ch3302.files.1drv.com/y3mBg4qrvz3cM9HKyTrtAIb6MATj71Aoh53Sx_FRDeGy7bGX_qh3DIQzt9ffUlfpDOGZ1tfA5Oa-74yTQqMKOFchX9iooXnuVcVM_6-FlBcR0_f6DM2Iv41P-Nn9rim0sL50nbAa6SYZvIZdjaFYKeil4i_hAQFNJfVO9MeRkp-tv0?width=256&height=199&cropmode=none)

Once the job has been successfully deployed, your **Output** tab in Visual Studio should output something similar to:

![Successful WebJob Deployment](https://0hpima-ch3302.files.1drv.com/y3mpe4ryzNH8Feie5hcHptjvWJu8L4tt8MgV9thod5tFM0ZeCyyrGL-i_AuM3O360z3MQnkNi6Vgu0o4vibapRuv5KAwxWmoQleB7OErMw5mRgaC3nB5D9BREsmZMHhSYcHtRseEDk3yVLs9N8iL0vCT28j2eAQnRviG3V5dtnnnPA?width=660&height=135&cropmode=none)

### Executing the WebJob

Now that you have deployed the WebJob, you want to turn the simulated sensor on.  Do this by logging into the Azure portal and selecting the App Service hosting the Azure WebJobs configuration.  Once you have selected the App Service, navigate to the **WebJobs** menu listed uner the **All Settings** menu.

![WebJobs Menu - All Settings](https://0hphma-ch3302.files.1drv.com/y3mF3Dbi4bvFK9Q16gGR5EpQm5iAS91nhuamnK98kMRN9u-AP_UZyrC4FWmOVaR_c6zmK6qw-VKEWMdkTPRErMl7SUSeO0VDWcMKkAtyU12Je79k2VSDNGxgpIx4jS_8GwEk5oz9AYAPgGoabkx7FYCndS1Mzi6J1aefkXQCVfPX34?width=660&height=495&cropmode=none)

Once you click on this, you will see a line item for each WebJob.  You should only see one at the present time.  Click on this WebJob, and under the **Logs** section is a URL.  Click on this URL, this will launch the WebJobs management UI.

![WebJob Management UI](https://z3ppma-ch3302.files.1drv.com/y3mc3NZksGzIpv_LI1CPn0ur65U6vdboHj8uKFyX0_ZnVLjbJdUlco-V2sp13Wp11cYsFH5EZynHd9-VrxWJkp4t-5tBcURoMcwRhBpeQzDxadOIZeZMFBQjk3-zGD8cDSiSfmuBaAUG3EQ1SP1e35wOxaBSIXii98LuJbQtyvZN78?width=660&height=235&cropmode=none)

From this page, at the very top you should see a link:  **WebJobs**, click on this link.  You will be taken to a screen showing you the current status of the WebJob and when it was last executed.  Since you have never ran this job, you will see a **Last Run Time** of:  **"Never Ran"**.  

![WebJob Management UI Job Detail](https://z3psma-ch3302.files.1drv.com/y3mMMNaN3bOInQEfJ2iLq2Vrxvx9GYxPsNRbxTg7Eu7dfJYOvKhYyUaOQFoJvQiXrjTzmVQTm9_r9LObr8JSxE7bCRcJeyR2NV2THLRajOR6HpDXkQ4ARhiS4irFkjK31YpIn0VmooetwdxsT3GfyqjheLT1TBHJvHliVvehfW3a3o?width=660&height=115&cropmode=none)

Click on the **SimulatedDevice** WebJob.  Start the job.


[//]: # (These are reference links used in the body of this note and get stripped out when the markdown processor does its job. There is no need to format nicely because it shouldn't be seen. Thanks SO - http://stackoverflow.com/questions/4823468/store-comments-in-markdown-syntax)


   [Windows 10 Mobile Emulator]: <http://go.microsoft.com/fwlink/?LinkId=698769>
   [Windows 10 SDK]: <http://go.microsoft.com/fwlink/?LinkID=698771>
   [Azure SDK for Visual Studio 2015]: <https://go.microsoft.com/fwlink/?linkid=746481&clcid=0x409>
   [Azure WebJob]: <https://azure.microsoft.com/en-us/documentation/articles/websites-webjobs-resources/>