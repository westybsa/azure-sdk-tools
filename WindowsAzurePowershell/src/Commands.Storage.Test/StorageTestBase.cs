﻿﻿// ----------------------------------------------------------------------------------
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
// -----------------------------------------------------------------------------------

namespace Microsoft.WindowsAzure.Commands.Storage.Test
{
    using System;
    using System.Diagnostics;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Test base class for storage package
    /// </summary>
    public class StorageTestBase
    {
        /// <summary>
        /// Get an unique string
        /// </summary>
        /// <param name="prefix">unique string prefix</param>
        /// <param name="replaceBar">replace "-" to the specific character</param>
        /// <returns>an unique string</returns>
        public static string GetUniqueString(string prefix = "", string replaceBar = "-")
        { 
            string name = prefix + System.Guid.NewGuid().ToString().Replace("-", replaceBar);
            return name;
        }

        /// <summary>
        /// Ensure an action throws a specific type of Exception.
        /// </summary>
        /// <typeparam name="T">Expected exception type.</typeparam>
        /// <param name="action">
        /// The action that should throw when executed.
        /// </param>
        public static void AssertThrows<T>(Action action)
            where T : Exception
        {
            Debug.Assert(action != null);

            try
            {
                action();
                Assert.Fail("No exception was thrown!");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(T));
            }
        }

        /// <summary>
        /// Ensure an action throws a specific type of Exception.
        /// </summary>
        /// <typeparam name="T">Expected exception type.</typeparam>
        /// <param name="action">
        /// The action that should throw when executed.
        /// </param>
        /// <param name="expectedMessage">
        /// Expected exception message.
        /// </param>
        public static void AssertThrows<T>(Action action, string expectedMessage)
            where T : Exception
        {
            Debug.Assert(action != null);

            try
            {
                action();
                Assert.Fail("No exception was thrown!");
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex, typeof(T));
                Assert.AreEqual<string>(expectedMessage, ex.Message);
            }
        }
    }
}