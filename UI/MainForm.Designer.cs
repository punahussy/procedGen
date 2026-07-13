namespace ProcedGenV2.UI;

partial class MainForm
{
    /// <summary>Обязательная переменная конструктора.</summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>Освобождает все используемые ресурсы.</summary>
    /// <param name="disposing">true, если управляемые ресурсы должны быть освобождены.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Код, автоматически созданный конструктором форм Windows

    /// <summary>
    /// Требуемый метод для поддержки конструктора — не изменяйте
    /// содержимое этого метода с помощью редактора кода.
    /// </summary>
    private void InitializeComponent()
    {
        this.SuspendLayout();
        //
        // MainForm
        //
        this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(1040, 660);
        this.MinimumSize = new System.Drawing.Size(680, 480);
        this.KeyPreview = true;
        this.Name = "MainForm";
        this.Text = "Procedural Level Generator";
        this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
        this.ResumeLayout(false);
    }

    #endregion
}
