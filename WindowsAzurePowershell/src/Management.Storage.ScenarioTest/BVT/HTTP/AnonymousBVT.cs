// ----------------------------------------------------------------------------------
//
// Copyright Microsoft Corporation
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// http://www.apache.org/licenses/LICENSE-2.0
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ----------------------------------------------------------------------------------

using Management.Storage.ScenarioTest.Common;
using Management.Storage.ScenarioTest.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MS.Test.Common.MsTestLib;

namespace Management.Storage.ScenarioTest.BVT.HTTP
{
    /// <summary>
    /// bvt cases for anonymous storage account
    /// </summary>
    [TestClass]
    class AnonymousBVT : Management.Storage.ScenarioTest.BVT.HTTPS.AnonymousBVT
    {
        [ClassInitialize()]
        public static void AnonymousHTTPBVTClassInitialize(TestContext testContext)
        {
            TestBase.TestClassInitialize(testContext);
            string StorageAccountName = Test.Data.Get("StorageAccountName");
            useHttps = false;
            PowerShellAgent.SetAnonymousStorageContext(StorageAccountName, useHttps);
            downloadDirRoot = Test.Data.Get("DownloadDir");
            SetupDownloadDir();
        }

        [ClassCleanup()]
        public static void AnonymousHTTPBVTClassCleanup()
        {
            FileUtil.CleanDirectory(downloadDirRoot);
            TestBase.TestClassCleanup();
        }
    }
}