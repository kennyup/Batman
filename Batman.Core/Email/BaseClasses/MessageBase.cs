﻿/*
Copyright (c) 2013 <a href="http://www.gutgames.com">James Craig</a>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.*/

#region Usings
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Batman.Core.Bootstrapper.Interfaces;
using Utilities.Reflection.ExtensionMethods;
using Utilities.DataTypes.ExtensionMethods;
using Batman.Core.Logging.BaseClasses;
using Utilities.IO.Logging.Enums;
using Batman.Core.Logging;
using System.IO;
using Batman.Core.Tasks;
using Batman.Core.Tasks.Enums;
using Batman.Core.FileSystem;
using System.Net.Mail;
using Batman.Core.Email.Interfaces;
using System.Threading;
using System.Net.Mime;
#endregion

namespace Batman.Core.Email.BaseClasses
{
    /// <summary>
    /// Email message base class
    /// </summary>
    public abstract class MessageBase : IMessage
    {
        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        /// <param name="Formatters">Formatters</param>
        protected MessageBase(IEnumerable<IFormatter> Formatters,EmailManager Manager)
        {
            Attachments = new List<Attachment>();
            EmbeddedResources = new List<LinkedResource>();
            Priority = MailPriority.Normal;
            this.Formatters = Formatters;
            this.Manager = Manager;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Any attachments that are included with this
        /// message.
        /// </summary>
        public ICollection<Attachment> Attachments { get; private set; }

        /// <summary>
        /// Formatters
        /// </summary>
        public IEnumerable<IFormatter> Formatters { get; private set; }

        /// <summary>
        /// Any attachment (usually images) that need to be embedded in the message
        /// </summary>
        public ICollection<LinkedResource> EmbeddedResources { get; private set; }

        /// <summary>
        /// The priority of this message
        /// </summary>
        public MailPriority Priority { get; set; }

        /// <summary>
        /// Email manager
        /// </summary>
        protected EmailManager Manager { get; private set; }

        /// <summary>
        /// Server Location
        /// </summary>
        public string Server { get; set; }

        /// <summary>
        /// User Name for the server
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Password for the server
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Port to send the information on
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Decides whether we are using STARTTLS (SSL) or not
        /// </summary>
        public bool UseSSL { get; set; }

        /// <summary>
        /// Carbon copy send (seperate email addresses with a comma)
        /// </summary>
        public string CC { get; set; }

        /// <summary>
        /// Blind carbon copy send (seperate email addresses with a comma)
        /// </summary>
        public string Bcc { get; set; }

        /// <summary>
        /// Whom the message is to
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// The subject of the email
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Whom the message is from
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// Body of the text
        /// </summary>
        public string Body { get; set; }

        #endregion

        #region Functions

        /// <summary>
        /// Formats the message
        /// </summary>
        /// <typeparam name="T">Model type</typeparam>
        /// <param name="Template">Template string</param>
        /// <param name="Model">Model object</param>
        public void Format<T>(string Template, T Model)
        {
            foreach (IFormatter Formatter in Formatters)
            {
                Formatter.Format(Template, this, Model);
            }
        }

        /// <summary>
        /// Sends the message
        /// </summary>
        public void Send()
        {
            Manager.SendMail(this);
        }

        /// <summary>
        /// Sends the message asynchronously
        /// </summary>
        public void SendAsync()
        {
            Manager.SendMailAsync(this);
        }

        /// <summary>
        /// Disposes the object
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes of the objects
        /// </summary>
        /// <param name="Disposing">True to dispose of all resources, false only disposes of native resources</param>
        protected virtual void Dispose(bool Disposing)
        {
            if (Attachments != null)
            {
                Attachments.ForEach(x => x.Dispose());
                Attachments = null;
            }
            if (EmbeddedResources != null)
            {
                EmbeddedResources.ForEach(x => x.Dispose());
                EmbeddedResources = null;
            }
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~MessageBase()
        {
            Dispose(false);
        }

        #endregion
    }
}