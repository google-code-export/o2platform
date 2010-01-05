/*
 * The contributors of Merlin license this file to You under the Apache 
 * License, Version 2.0 (the "License"); you may not use this file except 
 * in compliance with the License.  You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Merlin
{
    /// <summary>
    /// A blank step around a provided UI. Can be extended by inheritence
    /// and by composition by providing handler and button enablement strategy
    /// delegates.
    /// </summary>
    public class TemplateStep : IStep
    {
        /// <summary>
        /// A delegate for a void function with no arguments
        /// </summary>
        public delegate void VoidDelegate();
        
        /// <summary>
        /// A delegate for a function with no arguments that returns a boolean.
        /// </summary>
        public delegate bool BoolDelegate();

        /// <summary>
        /// Creates a new TemplateStep.
        /// </summary>
        /// <param name="ui"></param>
        public TemplateStep(Control ui)
            : this()
        {
            this.UI = ui;
        }

        // DC add support for just defining the Type
        public TemplateStep(Type uiType)
            : this()
        {
            this.UIType = uiType;
        }

        /// <summary>
        /// Empty constructor for use when the inheriting class will provide a UI
        /// at a later time (typically later in the constructor). If a UI has not
        /// been provided by the time the step is reached, a MissingUIException
        /// will be thrown
        /// </summary>
        protected TemplateStep()
        {
        }

        /// <summary>
        /// Creates a new template step
        /// </summary>
        /// <param name="ui"></param>
        /// <param name="title">The title of the new step.</param>
        public TemplateStep(Control ui, string title)
            : this(ui)
        {
            this.Title = title;
        }

        /// <summary>
        /// Creates a new template step with margins around the UI.
        /// </summary>
        /// <param name="ui"></param>
        /// <param name="margins"></param>
        public TemplateStep(Control ui, Padding margins)
            : this(padControl(ui, margins))
        { }

        /// <summary>
        /// Creates a new template step with margins around the UI.
        /// </summary>
        /// <param name="ui"></param>
        /// <param name="margins"></param>
        public TemplateStep(Control ui, int margins)
            : this(ui, new Padding(margins))
        { }

        /// <summary>
        /// Creates a new template step with the provided margins and title
        /// </summary>
        /// <param name="ui"></param>
        /// <param name="margins"></param>
        /// <param name="title"></param>
        public TemplateStep(Control ui, Padding margins, string title)
            : this(ui, margins)
        {
            this.Title = title;
        }

        public TemplateStep(Control ui, int margins, string title) 
            :this (ui, new Padding(margins), title)
        { }



        #region IStep Members

        /// <summary>
        /// The title of the step that will appear in the titlebar.
        /// </summary>
        public string Title
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the user-facing subtitle of this step. If the subtitle is not 
        /// null or empty, it appears beneath the title in the header section of the wizard
        /// </summary>
        public string Subtitle {
            get; 
            set; 
        }

        #region Handlers

        /// <summary>
        /// Gets and sets the handler that executes when the step is reached
        /// </summary>
        public VoidDelegate StepReachedHandler { get; set; }
        public virtual void StepReached()
        {
            if (this.UI == null)
            {
                throw new MissingUIException();
            }
            if (StepReachedHandler != null)
            {
                StepReachedHandler();
            }
        }

        /// <summary>
        /// Gets and sets the handler that executes when the Next button is clicked.
        /// </summary>
        public BoolDelegate NextHandler { get; set; }
        public virtual bool OnNext()
        {
            if (NextHandler != null)
            {
                return NextHandler();
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Gets and sets the handler that executes when the Previous button is clicked.
        /// </summary>
        public BoolDelegate PreviousHandler { get; set; }
        public virtual bool OnPrevious()
        {
            if (PreviousHandler != null)
            {
                return PreviousHandler();
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// A shortcut to firing the StepStateChanged event.
        /// </summary>
        public void StateUpdated()
        {
            StepStateChanged(this, EventArgs.Empty);
        }

        /// <summary>
        /// Gets and sets the handler that executes when the Cancel button is clicked.
        /// </summary>
        public VoidDelegate CancelHandler { get; set; }
        public virtual void OnCancel()
        {
            CancelHandler();
        }

        #endregion
        #region Button Permissions

        /// <summary>
        /// A boolean function to determine whether the "Next" button is enabled.
        /// </summary>
        public BoolDelegate AllowNextStrategy { get; set; }
        public virtual bool AllowNext()
        {
            if (AllowNextStrategy != null)
            {
                return AllowNextStrategy();
            }
            else
            {
                return true;
            } 
        }

        /// <summary>
        /// A boolean function to determine whether the "Previous" button is enabled.
        /// </summary>
        public BoolDelegate AllowPreviousStrategy { get; set; }
        public virtual bool AllowPrevious()
        {
            if (AllowPreviousStrategy != null)
            {
                return AllowPreviousStrategy();
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// A boolean function to determine whether the "Cancel" button is enabled.
        /// </summary>
        public BoolDelegate AllowCancelStrategy { get; set; }
        public virtual bool AllowCancel()
        {
            if (AllowCancelStrategy != null)
            {
                return AllowCancelStrategy();
            }
            else
            {
                return true;
            }
        }

        #endregion

        public event EventHandler StepStateChanged;

        public Control UI { get; set; }
        public Type UIType { get; set; }

        #endregion

        private static Panel padControl(Control ui, Padding margins)
        {
            if (ui is Panel)            // DC : exception case when ui is a Panel
            {
                ui.Parent = ui;         // DC: check if this recursive mapping has side effects
                return (Panel)ui;
            }
            else
            {
                Panel result = new Panel();
                result.Controls.Add(ui);
                ui.Parent = result;
                result.Resize += (object sender, EventArgs args) =>
                {
                    ui.Top = margins.Top;
                    ui.Left = margins.Left;
                    ui.Height = result.ClientRectangle.Height - margins.Vertical;
                    ui.Width = result.ClientRectangle.Width - margins.Horizontal;
                };
                return result;
            }            
        }

        /// <summary>
        /// Thrown if an inheriting class has not provided a UI by the time the
        /// step has been reached.
        /// </summary>
        public class MissingUIException
            : Exception
        {
            public MissingUIException()
                : base("UI has not been specified for this step")
            { }
        }

        // DC
        public Action<IStep> OnComponentAction { get; set; }
        public Action<IStep> OnComponentLoad { get; set; }
        public Object Model { get; set; }        
        public WizardController Controller { get; set; }
        public Control FirstControl { get; set; }
        public List<Control> Controls { get; set; }

        
    }
}
