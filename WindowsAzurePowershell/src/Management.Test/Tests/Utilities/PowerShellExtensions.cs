﻿// ----------------------------------------------------------------------------------
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

namespace Microsoft.WindowsAzure.Management.Test.Tests.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Management.Automation;

    public static class PowerShellExtensions
    {
        /// <summary>
        /// Gets a powershell varibale from the current session and convernts it back to it's original type.
        /// </summary>
        /// <typeparam name="T">The powershell object original type</typeparam>
        /// <param name="powershell">The PowerShell instance</param>
        /// <param name="name">The variable name</param>
        /// <returns>The variable object</returns>
        public static T GetPowerShellVariable<T>(this PowerShell powershell, string name)
        {
            PSObject psobject = powershell.Runspace.SessionStateProxy.GetVariable(name) as PSObject;
            return (T)psobject.BaseObject;
        }

        /// <summary>
        /// Logs a PowerShell exception thrown from PowerShell.Invoke, parsing the inner 
        /// PowerShell error record if available
        /// </summary>
        /// <param name="runtimeException">The exception to parse</param>
        public static void LogPowerShellException(this PowerShell powershell, Exception runtimeException)
        {
            Console.WriteLine("Caught Exception: {0}", runtimeException);
            Console.WriteLine("Message: {0}", runtimeException.Message);
            IContainsErrorRecord recordContainer = runtimeException as IContainsErrorRecord;
            if (recordContainer != null)
            {
                ErrorRecord record = recordContainer.ErrorRecord;
                Console.WriteLine("PowerShell Error Record: {0}\nException:{1}\nDetails:{2}\nScript Stack Trace: {3}\n: Target: {4}", record, record.Exception, record.ErrorDetails, record.ScriptStackTrace, record.TargetObject);
            }
            if (runtimeException.InnerException != null)
            {
                powershell.LogPowerShellException(runtimeException.InnerException);
            }
        }

        /// <summary>
        /// Log the PowerShell Streams from a PowerShell invocation
        /// </summary>
        /// <param name="powershell">The PowerShell instance to log</param>
        public static void LogPowerShellResults(this PowerShell powershell)
        {
            powershell.LogPowerShellResults(null);
        }

        /// <summary>
        /// Log the PowerShell Streams from a PowerShell invocation
        /// </summary>
        /// <param name="powershell">The PowerShell instance to log</param>
        public static void LogPowerShellResults(this PowerShell powershell, Collection<PSObject> output)
        {
            if (output != null)
            {
                LogPowerShellStream<PSObject>(output, "OUTPUT");
            }
            LogPowerShellStream<DebugRecord>(powershell.Streams.Debug, "DEBUG");
            LogPowerShellStream<ErrorRecord>(powershell.Streams.Error, "ERROR");
            LogPowerShellStream<ProgressRecord>(powershell.Streams.Progress, "PROGRESS");
            LogPowerShellStream<VerboseRecord>(powershell.Streams.Verbose, "VERBOSE");
            LogPowerShellStream<WarningRecord>(powershell.Streams.Warning, "WARNING");
        }

        /// <summary>
        /// Log a single PowerShell stream, using the given name
        /// </summary>
        /// <typeparam name="T">The type of the internal data record (different for every stream)</typeparam>
        /// <param name="stream">The stream to log</param>
        /// <param name="name">The name of the stream to print in the log</param>
        private static void LogPowerShellStream<T>(ICollection<T> stream, string name)
        {
            if (stream != null && stream.Count > 0)
            {
                Console.WriteLine("---------------------------------------------------------------");
                Console.WriteLine("{0} STREAM", name);
                Console.WriteLine("---------------------------------------------------------------");
                foreach (T item in stream)
                {
                    Console.WriteLine(item.ToString());
                }
                Console.WriteLine("---------------------------------------------------------------");
                Console.WriteLine();
            }
        }
    }
}