/* Software License Agreement (BSD License)
 * 
 * Copyright (c) 2010-2011, Rustici Software, LLC
 * All rights reserved.
 * 
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *     * Redistributions of source code must retain the above copyright
 *       notice, this list of conditions and the following disclaimer.
 *     * Redistributions in binary form must reproduce the above copyright
 *       notice, this list of conditions and the following disclaimer in the
 *       documentation and/or other materials provided with the distribution.
 *     * Neither the name of the <organization> nor the
 *       names of its contributors may be used to endorse or promote products
 *       derived from this software without specific prior written permission.
 * 
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
 * ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL Rustici Software, LLC BE LIABLE FOR ANY
 * DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
 * (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
 * LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
 * ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
 * SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Xml;

namespace RusticiSoftware.HostedEngine.Client
{
    /// <summary>
    /// Client-side proxy for the "rustici.debug.*" Hosted SCORM Engine web
    /// service methods.  
    /// </summary>
    public class DebugService
    {
        private Configuration configuration = null;
        private ScormEngineService manager = null;

        /// <summary>
        /// Main constructor that provides necessary configuration information
        /// </summary>
        /// <param name="configuration">Application Configuration Data</param>
        public DebugService(Configuration configuration, ScormEngineService manager)
        {
            this.configuration = configuration;
            this.manager = manager;
        }

        /// <summary>
        /// Check for a basic ping to the cloud server to ensure a successful connection
        /// </summary>
        /// <returns></returns>
        public bool CloudPing()
        {
            ServiceRequest request = new ServiceRequest(configuration);
            XmlDocument response = request.CallService("rustici.debug.ping");
            return response.DocumentElement.Attributes["stat"].Value.Equals("ok");
        }

        /// <summary>
        /// Check for an Authenticated ping to the cloud server to ensure valid credentials
        /// </summary>
        /// <returns></returns>
        public bool CloudAuthPing()
        {
            ServiceRequest request = new ServiceRequest(configuration);
            XmlDocument response = request.CallService("rustici.debug.authPing");
            return response.DocumentElement.Attributes["stat"].Value.Equals("ok");
        }


    }
}
