<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DialogExpExcel
    Inherits Telerik.WinControls.UI.RadForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(DialogExpExcel))
        Me.Barra = New Telerik.WinControls.UI.RadProgressBar()
        Me.TemaGeneral = New Telerik.WinControls.Themes.MaterialTheme()
        CType(Me.Barra, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Barra
        '
        Me.Barra.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Barra.Location = New System.Drawing.Point(4, 8)
        Me.Barra.Name = "Barra"
        Me.Barra.Size = New System.Drawing.Size(490, 37)
        Me.Barra.TabIndex = 0
        Me.Barra.Text = "Por Favor Espere..."
        Me.Barra.ThemeName = "Aqua"
        '
        'DialogExpExcel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(503, 50)
        Me.ControlBox = False
        Me.Controls.Add(Me.Barra)
        Me.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.IconScaling = Telerik.WinControls.Enumerations.ImageScaling.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "DialogExpExcel"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = ""
        Me.ThemeName = "Material"
        CType(Me.Barra, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Barra As Telerik.WinControls.UI.RadProgressBar
    Friend WithEvents TemaGeneral As Telerik.WinControls.Themes.MaterialTheme
End Class

