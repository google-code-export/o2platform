/* Copyright (c) 2007 Google Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
*/

using System;
using System.Text;
using Google.GData.Client;
using Google.GData.Extensions.Apps;

namespace Google.GData.Apps.GoogleMailSettings
{
    /// <summary>
    /// Base service for accessing Google Mail Settings item feeds from the
    /// Google Apps Google Mail Settings API.
    /// </summary>
    public class GoogleMailSettingsService : Service
    {
        /// <summary>
        /// URL suffixes for the Google Mail Settings tasks
        /// </summary>
        public const string labelFeedUriSuffix = "/label";
        public const string filterFeedUriSuffix = "/filter";
        public const string sendasFeedUriSuffix = "/sendas";
        public const string forwardingFeedUriSuffix = "/forwarding";
        public const string popFeedUriSuffix = "/pop";
        public const string imapFeedUriSuffix = "/imap";
        public const string vacationFeedUriSuffix = "/vacation";
        public const string signatureFeedUriSuffix = "/signature";
        public const string languageFeedUriSuffix = "/language";
        public const string generalFeedUriSuffix = "/general";

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="domain">The hosted domain in which the Google Mail Settings are
        /// being set up</param>
        /// <param name="applicationName">The name of the client application 
        /// using this service.</param>
        public GoogleMailSettingsService(string domain, string applicationName)
            : base(AppsNameTable.GAppsService, applicationName)
        {
            this.domain = domain;
            this.NewAtomEntry += new FeedParserEventHandler(this.OnParsedNewGoogleMailSettingsItemEntry);
            this.NewFeed += new ServiceEventHandler(this.OnNewFeed);
            // You can set factory.methodOverride = true if you are behind a 
            // proxy that filters out HTTP methods such as PUT and DELETE.
        }

        private string domain;

        /// <summary>
        /// Accessor for Domain property.
        /// </summary>
        public string Domain
        {
            get { return domain; }
        }

        /// <summary>
        /// Creates a new Google Mail label for the given userName
        /// </summary>
        /// <param name="userName">The user for whom this should be done</param>
        /// <param name="label">the new Google Mail label</param>
        /// <returns>a <code>GoogleMailSettingsEntry</code> containing the results of the
        /// creation</returns>
        public GoogleMailSettingsEntry CreateLabel(string userName, string label)
        {
            Uri labelUri = new Uri(AppsGoogleMailSettingsNameTable.AppsGoogleMailSettingsBaseFeedUri + "/"
               + domain + "/" + userName + labelFeedUriSuffix);
            GoogleMailSettingsEntry entry = new GoogleMailSettingsEntry();
            entry.Properties.Add(
                new PropertyElement(AppsGoogleMailSettingsNameTable.label,
                label));
            return base.Insert(labelUri, entry) as GoogleMailSettingsEntry;
        }

        /// <summary>
        /// Creates a new Google Mail filter for the given userName
        /// </summary>
        /// <param name="userName">The user for whom this should be done</param>
        /// <param name="from">come-from email address to be filtered</param>
        /// <param name="to">send-to email address to be filtered</param>
        /// <param name="subject">a string the email must have on the subject line to be filtered</param>
        /// <param name="hasTheWords">a string the email can have anywhere in its subject or body</param>
        /// <param name="doesNotHaveTheWords">a string the email cannot have anywhere in its subject or body</param>
        /// <param name="hasAttachment">a boolean representing whether or not the emails contains an attachment</param>
        /// <param name="label">the name of the label to apply if the message matches the filter criteria</param>
        /// <param name="shouldMarkAsRead">Whether to automatically mark the message as read
        /// if the message matches the filter criteria</param>
        /// <param name="shouldArchive">Whether to automatically move the message to Archived state
        /// if the message matches the filter criteria</param>
        /// <returns>a <code>GoogleMailSettingsEntry</code> containing the results of the
        /// creation</returns>
        public GoogleMailSettingsEntry CreateFilter(string userName, string from, string to,
            string subject, string hasTheWords, string doesNotHaveTheWords, string hasAttachment,
            string label, string shouldMarkAsRead, string shouldArchive)
        {
            Uri fitlerUri = new Uri(AppsGoogleMailSettingsNameTable.AppsGoogleMailSettingsBaseFeedUri + "/"
                + domain + "/" + userName + filterFeedUriSuffix);
            GoogleMailSettingsEntry entry = new GoogleMailSettingsEntry();
            if (from != null && from != String.Empty)
                entry.Properties.Add(
                    new PropertyElement(
                    AppsGoogleMailSettingsNameTable.from, from));
            if (to != null && to != String.Empty)
                entry.Properties.Add(
                    new PropertyElement(
                    AppsGoogleMailSettingsNameTable.to, to));
            if (subject != null && subject != String.Empty)
                entry.Properties.Add(
                    new PropertyElement(
                    AppsGoogleMailSettingsNameTable.subject, subject));
            if (hasTheWords != null && hasTheWords != String.Empty)
                entry.Properties.Add(
                    new PropertyElement(
                    AppsGoogleMailSettingsNameTable.hasTheWord, hasTheWords));
            if (doesNotHaveTheWords != null && doesNotHaveTheWords != String.Empty)
                entry.Properties.Add(
                    new PropertyElement(
                    AppsGoogleMailSettingsNameTable.doesNotHaveTheWord,
                    doesNotHaveTheWords));
            if (hasAttachment != null && hasAttachment != String.Empty)
                entry.Properties.Add(
                    new PropertyElement(
                    AppsGoogleMailSettingsNameTable.hasAttachment, hasAttachment));
            if (label != null && label != String.Empty)
                entry.Properties.Add(
                    new PropertyElement(
                    AppsGoogleMailSettingsNameTable.label, label));
            if (shouldMarkAsRead != null && shouldMarkAsRead != String.Empty)
                entry.Properties.Add(
                    new PropertyElement(
                    AppsGoogleMailSettingsNameTable.shouldMarkAsRead,
                    shouldMarkAsRead));
            if (shouldArchive != null && shouldArchive != String.Empty)
                entry.Properties.Add(
                    new PropertyElement(
                    AppsGoogleMailSettingsNameTable.shouldArchive, shouldArchive));
            return base.Insert(fitlerUri, entry) as GoogleMailSettingsEntry;
        }

        /// <summary>
        /// Creates a new Google Mail senda-as alias for the given userName
        /// </summary>
        /// <param name="userName">The user for whom this should be done</param>
        /// <param name="name">The name which emails sent using the alias are from</param>
        /// <param name="address">The email address which emails sent using the alias are from</param>
        /// <param name="replyTo">If set, this address will be included as the reply-to addres for the alias</param>
        /// <param name="makeDefault">Whether the new alias would be the default email address</param>
        /// <returns>a <code>GoogleMailSettingsEntry</code> containing the results of the
        /// creation</returns>
        public GoogleMailSettingsEntry CreateSendAs(string userName, string name, string address, string replyTo,
            string makeDefault)
        {
            Uri sendaslUri = new Uri(AppsGoogleMailSettingsNameTable.AppsGoogleMailSettingsBaseFeedUri + "/"
                + domain + "/" + userName + sendasFeedUriSuffix);
            GoogleMailSettingsEntry entry = new GoogleMailSettingsEntry();
            entry.Properties.Add(
                new PropertyElement(
                AppsGoogleMailSettingsNameTable.name, name));
            entry.Properties.Add(
                new PropertyElement(
                AppsGoogleMailSettingsNameTable.address, address));
            if (replyTo != null && replyTo != String.Empty)
                entry.Properties.Add(
                    new PropertyElement(
                    AppsGoogleMailSettingsNameTable.replyTo, replyTo));
            if (makeDefault != null && makeDefault != String.Empty)
                entry.Properties.Add(
                    new PropertyElement(
                    AppsGoogleMailSettingsNameTable.makeDefault, makeDefault));
            return base.Insert(sendaslUri, entry) as GoogleMailSettingsEntry;
        }

        /// <summary>
        /// Updates Google Mail's forwarding rule for the given userName
        /// </summary>
        /// <param name="userName">The user for whom this should be done</param>
        /// <param name="enable">Whether to enable forwarding of incoming mail</param>
        /// <param name="forwardTo">The email will be forwarded to this address</param>
        /// <param name="action">What Google Mail should do with its copy of the email after forwarding it on</param>
        /// <returns>a <code>GoogleMailSettingsEntry</code> containing the results of the
        /// creation</returns>
        public GoogleMailSettingsEntry UpdateForwarding(string userName, string enable, string forwardTo, string action)
        {
            Uri forwardingUri = new Uri(AppsGoogleMailSettingsNameTable.AppsGoogleMailSettingsBaseFeedUri + "/"
                + domain + "/" + userName + forwardingFeedUriSuffix);
            GoogleMailSettingsEntry entry = new GoogleMailSettingsEntry();
            entry.EditUri = forwardingUri;
            entry.Properties.Add(
                new PropertyElement(
                AppsGoogleMailSettingsNameTable.enable, enable));
            if (forwardTo != null && forwardTo != String.Empty)
                entry.Properties.Add(
                    new PropertyElement(
                    AppsGoogleMailSettingsNameTable.forwardTo, forwardTo));
            if (action != null && action != String.Empty)
                entry.Properties.Add(
                    new PropertyElement(
                    AppsGoogleMailSettingsNameTable.action, action));
            return base.Update((AtomEntry)entry) as GoogleMailSettingsEntry;
        }

        /// <summary>
        /// Updates Google Mail's POP settings for the given userName
        /// </summary>
        /// <param name="userName">The user for whom this should be done</param>
        /// <param name="enable">Whether to enable POP3 access</param>
        /// <param name="enableFor">Whether to enable POP3 for all mail or mail from now on</param>
        /// <param name="action">What Google Mail should do with its copy of the email after 
        /// it is retrieved using POP3</param>
        /// <returns>a <code>GoogleMailSettingsEntry</code> containing the results of the
        /// creation</returns>
        public GoogleMailSettingsEntry UpdatePop(string userName, string enable, string enableFor, string action)
        {
            Uri popUri = new Uri(AppsGoogleMailSettingsNameTable.AppsGoogleMailSettingsBaseFeedUri + "/"
                + domain + "/" + userName + popFeedUriSuffix);
            GoogleMailSettingsEntry entry = new GoogleMailSettingsEntry();
            entry.EditUri = popUri;
            entry.Properties.Add(
                new PropertyElement(
                AppsGoogleMailSettingsNameTable.enable, enable));
            if (enableFor != null && enableFor != String.Empty)
                entry.Properties.Add(
                    new PropertyElement(
                    AppsGoogleMailSettingsNameTable.enableFor, enableFor));
            if (action != null && action != String.Empty)
                entry.Properties.Add(
                    new PropertyElement(
                    AppsGoogleMailSettingsNameTable.action, action));
            return base.Update((AtomEntry)entry) as GoogleMailSettingsEntry;
        }

        /// <summary>
        /// Updates Google Mail's IMAP settings for the given userName
        /// </summary>
        /// <param name="userName">The user for whom this should be done</param>
        /// <param name="enable">Whether to enable IMAP access</param>
        /// <returns>a <code>GoogleMailSettingsEntry</code> containing the results of the
        /// creation</returns>
        public GoogleMailSettingsEntry UpdateImap(string userName, string enable)
        {
            Uri imapUri = new Uri(AppsGoogleMailSettingsNameTable.AppsGoogleMailSettingsBaseFeedUri + "/"
                + domain + "/" + userName + imapFeedUriSuffix);
            GoogleMailSettingsEntry entry = new GoogleMailSettingsEntry();
            entry.EditUri = imapUri;
            entry.Properties.Add(
                new PropertyElement(
                AppsGoogleMailSettingsNameTable.enable, enable));
            return base.Update((AtomEntry)entry) as GoogleMailSettingsEntry;
        }

        /// <summary>
        /// Updates Google Mail's vacation-responder settings for the given userName
        /// </summary>
        /// <param name="userName">The user for whom this should</param>
        /// <param name="enable">Wheter to enable the vacation responder</param>
        /// <param name="subject">The subject line of the vacacion responder autoresponse</param>
        /// <param name="message">The message body of the vacation responder autoresponse</param>
        /// <param name="contactsOnly">Wheter to only send the autoresponse to known contacts</param>
        /// <returns>a <code>GoogleMailSettingsEntry</code> containing the results of the
        /// creation</returns>
        public GoogleMailSettingsEntry UpdateVacation(string userName, string enable, string subject,
            string message, string contactsOnly)
        {
            Uri vacationUri = new Uri(AppsGoogleMailSettingsNameTable.AppsGoogleMailSettingsBaseFeedUri + "/"
                + domain + "/" + userName + vacationFeedUriSuffix);
            GoogleMailSettingsEntry entry = new GoogleMailSettingsEntry();
            entry.EditUri = vacationUri;
            entry.Properties.Add(
                new PropertyElement(
                AppsGoogleMailSettingsNameTable.enable, enable));
            if (subject != null && subject != String.Empty)
                entry.Properties.Add(
                    new PropertyElement(
                    AppsGoogleMailSettingsNameTable.subject, subject));
            if (message != null && message != String.Empty)
                entry.Properties.Add(
                    new PropertyElement(
                    AppsGoogleMailSettingsNameTable.message, message));
            if (contactsOnly != null && contactsOnly != String.Empty)
                entry.Properties.Add(
                    new PropertyElement(
                    AppsGoogleMailSettingsNameTable.contactsOnly, contactsOnly));
            return base.Update((AtomEntry)entry) as GoogleMailSettingsEntry;
        }

        /// <summary>
        /// Updates Google Mail's signature for the given userName
        /// </summary>
        /// <param name="userName">The user for whom this should be done</param>
        /// <param name="signature">The signature to be appended to outgoing messages</param>
        /// <returns>a <code>GoogleMailSettingsEntry</code> containing the results of the
        /// creation</returns>
        public GoogleMailSettingsEntry UpdateSignature(string userName, string signature)
        {
            Uri signatureUri = new Uri(AppsGoogleMailSettingsNameTable.AppsGoogleMailSettingsBaseFeedUri + "/"
                + domain + "/" + userName + signatureFeedUriSuffix);
            GoogleMailSettingsEntry entry = new GoogleMailSettingsEntry();
            entry.EditUri = signatureUri;
            if (signature == null)
                signature = String.Empty;
            entry.Properties.Add(
                new PropertyElement(
                AppsGoogleMailSettingsNameTable.signature, signature));
            return base.Update((AtomEntry)entry) as GoogleMailSettingsEntry;
        }

        /// <summary>
        /// Updates Google Mail's display language for the given userName
        /// </summary>
        /// <param name="userName">The user for whom this should be done</param>
        /// <param name="language">Google Mail's display language</param>
        /// <returns>a <code>GoogleMailSettingsEntry</code> containing the results of the
        /// creation</returns>
        public GoogleMailSettingsEntry UpdateLanguage(string userName, string language)
        {
            Uri languageUri = new Uri(AppsGoogleMailSettingsNameTable.AppsGoogleMailSettingsBaseFeedUri + "/"
                + domain + "/" + userName + languageFeedUriSuffix);
            GoogleMailSettingsEntry entry = new GoogleMailSettingsEntry();
            entry.EditUri = languageUri;
            entry.Properties.Add(
                new PropertyElement(
                AppsGoogleMailSettingsNameTable.language, language));
            return base.Update((AtomEntry)entry) as GoogleMailSettingsEntry;
        }

        /// <summary>
        /// Updates Google Mail's general settings for the given userName
        /// </summary>
        /// <param name="userName">The user for whom this should be done</param>
        /// <param name="label">the new Google Mail label</param>
        /// <returns>a <code>GoogleMailSettingsEntry</code> containing the results of the
        /// creation</returns>
        public GoogleMailSettingsEntry UpdateGeneralSettings(string userName, string pageSize, string shortcuts,
            string arrows, string snippets, string unicode)
        {
            Uri generalUri = new Uri(AppsGoogleMailSettingsNameTable.AppsGoogleMailSettingsBaseFeedUri + "/"
                + domain + "/" + userName + generalFeedUriSuffix);
            GoogleMailSettingsEntry entry = new GoogleMailSettingsEntry();
            entry.EditUri = generalUri;
            if (pageSize != null && pageSize != String.Empty)
                entry.Properties.Add(
                   new PropertyElement(
                   AppsGoogleMailSettingsNameTable.pageSize, pageSize));
            if (shortcuts != null && shortcuts != String.Empty)
                entry.Properties.Add(
                   new PropertyElement(
                   AppsGoogleMailSettingsNameTable.shortcuts, shortcuts));
            if (arrows != null && arrows != String.Empty)
                entry.Properties.Add(
                   new PropertyElement(
                   AppsGoogleMailSettingsNameTable.arrows, arrows));
            if (snippets != null && snippets != String.Empty)
                entry.Properties.Add(
                   new PropertyElement(
                   AppsGoogleMailSettingsNameTable.snippets, snippets));
            if (unicode != null && unicode != String.Empty)
                entry.Properties.Add(
                   new PropertyElement(
                   AppsGoogleMailSettingsNameTable.unicode, unicode));
            return base.Update((AtomEntry)entry) as GoogleMailSettingsEntry;
        }

        /// <summary>
        /// Event handler. Called when a new Google Mail Settings entry is parsed.
        /// </summary>
        /// <param name="sender">the object that's sending the evet</param>
        /// <param name="e">FeedParserEventArguments, holds the feedentry</param>
        protected void OnParsedNewGoogleMailSettingsItemEntry(object sender, FeedParserEventArgs e)
        {
            if (e == null)
            {
                throw new ArgumentNullException("e");
            }
            if (e.CreatingEntry == true)
            {
                e.Entry = new GoogleMailSettingsEntry();
            }
        }

        /// <summary>
        /// Overridden so that new feeds are returned as <code>GoogleMailSettingsFeed</code>s
        /// instead of base <code>AtomFeed</code>s.
        /// </summary>
        /// <param name="sender"> the object which sent the event</param>
        /// <param name="e">FeedParserEventArguments, holds the FeedEntry</param> 
        protected void OnNewFeed(object sender, ServiceEventArgs e)
        {
            Tracing.TraceMsg("Created new Google Mail Settings Item Feed");
            if (e == null)
            {
                throw new ArgumentNullException("e");
            }
            e.Feed = new GoogleMailSettingsFeed(e.Uri, e.Service);
        }
    }
}
