<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Acceso
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Acceso))
        Me.TemaGeneral = New Telerik.WinControls.Themes.MaterialTheme()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.TxtUsuario = New Telerik.WinControls.UI.RadTextBox()
        Me.TxtPass = New Telerik.WinControls.UI.RadTextBox()
        Me.OK = New Telerik.WinControls.UI.RadButton()
        Me.Cancel = New Telerik.WinControls.UI.RadButton()
        Me.PasswordLabel = New System.Windows.Forms.Label()
        Me.UsernameLabel = New System.Windows.Forms.Label()
        Me.Boton = New Telerik.WinControls.Themes.AquaTheme()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtUsuario, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtPass, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OK, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Cancel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = Global.ATMFiscal.My.Resources.Resources.Atmlogo
        Me.PictureBox1.InitialImage = Nothing
        Me.PictureBox1.Location = New System.Drawing.Point(25, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(289, 184)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 7
        Me.PictureBox1.TabStop = False
        '
        'TxtUsuario
        '
        Me.TxtUsuario.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtUsuario.Location = New System.Drawing.Point(25, 226)
        Me.TxtUsuario.Name = "TxtUsuario"
        Me.TxtUsuario.Size = New System.Drawing.Size(289, 38)
        Me.TxtUsuario.TabIndex = 0
        Me.TxtUsuario.ThemeName = "Material"
        '
        'TxtPass
        '
        Me.TxtPass.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPass.Location = New System.Drawing.Point(25, 309)
        Me.TxtPass.Name = "TxtPass"
        Me.TxtPass.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TxtPass.Size = New System.Drawing.Size(289, 38)
        Me.TxtPass.TabIndex = 1
        Me.TxtPass.ThemeName = "Material"
        '
        'OK
        '
        Me.OK.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OK.ForeColor = System.Drawing.Color.MidnightBlue
        Me.OK.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.OK.Location = New System.Drawing.Point(25, 362)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(107, 44)
        Me.OK.TabIndex = 2
        Me.OK.TabStop = False
        Me.OK.Text = "&Aceptar"
        Me.OK.ThemeName = "Aqua"
        '
        'Cancel
        '
        Me.Cancel.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Cancel.ForeColor = System.Drawing.Color.DarkRed
        Me.Cancel.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.Cancel.Location = New System.Drawing.Point(207, 362)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(107, 44)
        Me.Cancel.TabIndex = 3
        Me.Cancel.TabStop = False
        Me.Cancel.Text = "&Cancelar"
        Me.Cancel.ThemeName = "Aqua"
        '
        'PasswordLabel
        '
        Me.PasswordLabel.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PasswordLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.PasswordLabel.Location = New System.Drawing.Point(21, 276)
        Me.PasswordLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.PasswordLabel.Name = "PasswordLabel"
        Me.PasswordLabel.Size = New System.Drawing.Size(182, 28)
        Me.PasswordLabel.TabIndex = 13
        Me.PasswordLabel.Text = "&Contraseña:"
        Me.PasswordLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'UsernameLabel
        '
        Me.UsernameLabel.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UsernameLabel.ForeColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.UsernameLabel.Location = New System.Drawing.Point(21, 197)
        Me.UsernameLabel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.UsernameLabel.Name = "UsernameLabel"
        Me.UsernameLabel.Size = New System.Drawing.Size(205, 28)
        Me.UsernameLabel.TabIndex = 12
        Me.UsernameLabel.Text = "&Nombre de usuario:"
        Me.UsernameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Acceso
        '
        Me.AcceptButton = Me.OK
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.CadetBlue
        Me.ClientSize = New System.Drawing.Size(339, 419)
        Me.Controls.Add(Me.PasswordLabel)
        Me.Controls.Add(Me.UsernameLabel)
        Me.Controls.Add(Me.Cancel)
        Me.Controls.Add(Me.OK)
        Me.Controls.Add(Me.TxtPass)
        Me.Controls.Add(Me.TxtUsuario)
        Me.Controls.Add(Me.PictureBox1)
        Me.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Acceso"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Acceso"
        Me.ThemeName = "MaterialBlueGrey"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtUsuario, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtPass, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OK, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Cancel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TemaGeneral As Telerik.WinControls.Themes.MaterialTheme
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents TxtUsuario As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents TxtPass As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents OK As Telerik.WinControls.UI.RadButton
    Friend WithEvents Cancel As Telerik.WinControls.UI.RadButton
    Friend WithEvents PasswordLabel As Label
    Friend WithEvents UsernameLabel As Label
    Friend WithEvents Boton As Telerik.WinControls.Themes.AquaTheme
End Class

