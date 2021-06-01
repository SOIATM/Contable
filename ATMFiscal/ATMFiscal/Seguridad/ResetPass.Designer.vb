<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ResetPass
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ResetPass))
        Me.TemaGeneral = New Telerik.WinControls.Themes.MaterialTheme()
        Me.TxtPass2 = New Telerik.WinControls.UI.RadTextBox()
        Me.TxtPass = New Telerik.WinControls.UI.RadTextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Cmd_Procesar = New Telerik.WinControls.UI.RadButton()
        Me.CmdCerrar = New Telerik.WinControls.UI.RadButton()
        Me.lstUsuarios = New ATMFiscal.Listas()
        Me.Botones = New Telerik.WinControls.Themes.AquaTheme()
        CType(Me.TxtPass2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TxtPass, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Cmd_Procesar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdCerrar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TxtPass2
        '
        Me.TxtPass2.AutoSize = False
        Me.TxtPass2.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPass2.Location = New System.Drawing.Point(25, 255)
        Me.TxtPass2.Name = "TxtPass2"
        Me.TxtPass2.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TxtPass2.Size = New System.Drawing.Size(260, 36)
        Me.TxtPass2.TabIndex = 71
        Me.TxtPass2.ThemeName = "Material"
        '
        'TxtPass
        '
        Me.TxtPass.AutoSize = False
        Me.TxtPass.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPass.Location = New System.Drawing.Point(25, 188)
        Me.TxtPass.Name = "TxtPass"
        Me.TxtPass.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.TxtPass.Size = New System.Drawing.Size(260, 36)
        Me.TxtPass.TabIndex = 70
        Me.TxtPass.ThemeName = "Material"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(26, 88)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(64, 18)
        Me.Label3.TabIndex = 645
        Me.Label3.Text = "Usuario:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold)
        Me.Label2.Location = New System.Drawing.Point(26, 234)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(80, 18)
        Me.Label2.TabIndex = 644
        Me.Label2.Text = "Confirmar:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(26, 161)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(124, 18)
        Me.Label1.TabIndex = 643
        Me.Label1.Text = "Nuevo Password:"
        '
        'Cmd_Procesar
        '
        Me.Cmd_Procesar.Image = Global.ATMFiscal.My.Resources.Resources.OK32
        Me.Cmd_Procesar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.Cmd_Procesar.Location = New System.Drawing.Point(98, 12)
        Me.Cmd_Procesar.Name = "Cmd_Procesar"
        Me.Cmd_Procesar.Size = New System.Drawing.Size(67, 67)
        Me.Cmd_Procesar.TabIndex = 648
        Me.Cmd_Procesar.TabStop = False
        Me.Cmd_Procesar.TextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.Cmd_Procesar.ThemeName = "Aqua"
        '
        'CmdCerrar
        '
        Me.CmdCerrar.Image = CType(resources.GetObject("CmdCerrar.Image"), System.Drawing.Image)
        Me.CmdCerrar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdCerrar.Location = New System.Drawing.Point(25, 12)
        Me.CmdCerrar.Name = "CmdCerrar"
        Me.CmdCerrar.Size = New System.Drawing.Size(67, 67)
        Me.CmdCerrar.TabIndex = 647
        Me.CmdCerrar.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdCerrar.ThemeName = "Aqua"
        '
        'lstUsuarios
        '
        Me.lstUsuarios.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstUsuarios.Location = New System.Drawing.Point(25, 112)
        Me.lstUsuarios.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lstUsuarios.Name = "lstUsuarios"
        Me.lstUsuarios.SelectItem = ""
        Me.lstUsuarios.SelectText = ""
        Me.lstUsuarios.Size = New System.Drawing.Size(260, 36)
        Me.lstUsuarios.TabIndex = 646
        '
        'ResetPass
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(315, 321)
        Me.ControlBox = False
        Me.Controls.Add(Me.Cmd_Procesar)
        Me.Controls.Add(Me.CmdCerrar)
        Me.Controls.Add(Me.lstUsuarios)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TxtPass2)
        Me.Controls.Add(Me.TxtPass)
        Me.Name = "ResetPass"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Activar Usuario"
        Me.ThemeName = "MaterialBlueGrey"
        CType(Me.TxtPass2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TxtPass, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Cmd_Procesar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdCerrar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TemaGeneral As Telerik.WinControls.Themes.MaterialTheme
    Friend WithEvents TxtPass2 As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents TxtPass As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents lstUsuarios As Listas
    Friend WithEvents Cmd_Procesar As Telerik.WinControls.UI.RadButton
    Friend WithEvents CmdCerrar As Telerik.WinControls.UI.RadButton
    Friend WithEvents Botones As Telerik.WinControls.Themes.AquaTheme
End Class

