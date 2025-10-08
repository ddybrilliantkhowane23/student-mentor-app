using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentMentorApp
{
    public partial class MentorForm : Form
    {
        public MentorForm()
        {
            InitializeComponent();

            // Set the form's starting position and border style
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            // Set up the button styles
            SetupButtonStyles();

            // ===== AUTOMATIC CONTENT LOADING: Load advice when category selected =====

            // Event handler for category selection changes
            lstCategories.SelectedIndexChanged += lstCategories_SelectedIndexChanged;
        }


        
        private void SetupButtonStyles()
        {
            // Create an array of buttons to style
            Button[] buttons = { btnClear, btnExit }; 

            foreach (var btn in buttons)
            {
                btn.FlatStyle = FlatStyle.Flat;
                btn.FlatAppearance.BorderSize = 1;
                btn.BackColor = System.Drawing.Color.Aqua;

                // Add mouse enter event
                btn.MouseEnter += (s, e) => btn.BackColor = System.Drawing.Color.LightGray;
                
                // Add mouse leave event 
                btn.MouseLeave += (s, e) => btn.BackColor = System.Drawing.Color.Aqua;
            }
        }


        // ===== CORE FUNCTIONALITY: Read C++ generated file and display advice =====
        // This method demonstrates C++/C# integration through file I/O
        private void lstCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Check if a category is selected
            if (lstCategories.SelectedItem == null)
            {
                MessageBox.Show("Please select a category first.", "Notice");
                return;
            }

            string selectedCategory = lstCategories.SelectedItem.ToString();

            // ===== FILE PATH: Reading from same location as C++ output =====
            // This is the integration point between C++ backend and C# frontend
            string path = @"E:\advices.txt";  // USB drive path (must match C++ output)

            // ===== ERROR HANDLING: Check if C++ file exists =====
            if (!System.IO.File.Exists(path))
            {
                MessageBox.Show("advices.txt not found in the app folder.\n" +
                    "\nPlease run the C++ program first to generate advice file.", 
                    "File Missing");
                return;
            }

            lstAdvices.Items.Clear();  // Clear previous advice

            // ===== FILE READING: Process C++ generated content =====
            string[] lines = System.IO.File.ReadAllLines(path);
            bool categoryFound = false;

            foreach (string line in lines)
            {
                string trimmed = line.Trim();

                if (string.IsNullOrWhiteSpace(trimmed))
                    continue;

                // ===== CATEGORY DETECTION: Look for [CategoryName] format =====
                // This parsing logic matches the format created by C++ program
                if (trimmed.StartsWith("[") && trimmed.EndsWith("]"))
                {
                    // Extract category name from brackets [Study Tips] -> "Study Tips"
                    string categoryInFile = trimmed.Substring(1, trimmed.Length - 2);
                    categoryFound = (categoryInFile == selectedCategory);
                    continue;
                }

                // ===== CONTENT EXTRACTION: Add tips under selected category =====
                if (categoryFound)
                {
                    // Stop reading if we encounter another category
                    if (trimmed.StartsWith("[") && trimmed.EndsWith("]"))
                        break;

                    lstAdvices.Items.Add(trimmed);  // Display the advice
                }
            }

            // ===== USER FEEDBACK: Inform user of results =====
            if (lstAdvices.Items.Count == 0)
            {
                MessageBox.Show("No advices found for " + selectedCategory + ".\n\nMake " +
                    "sure you selected the same category in the C++ program.", "Notice");
            }
            else
            {
                MessageBox.Show("Advices loaded for " + selectedCategory + "!", "Done");
            }

        }

        // ===== CLEAR BUTTON: Reset displayed advice =====
        private void btnClear_Click(object sender, EventArgs e)
        {
            lstAdvices.Items.Clear();
            MessageBox.Show("Cleared.", "Notice");
        }

        // ===== EXIT BUTTON: Safe application closure =====
        private void btnExit_Click(object sender, EventArgs e)
        {
           // Ask the user to confirm exit
            DialogResult result = MessageBox.Show("Do you want to exit?", "Exit", 
                MessageBoxButtons.YesNo);
                
            if (result == DialogResult.Yes)
            {
                // Close the application
                Application.Exit();
            } 
        }

        
    }
}
