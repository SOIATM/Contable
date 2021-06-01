<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class DialogUnaSeleccion
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
        Me.TemaGeneral = New Telerik.WinControls.Themes.MaterialTheme()
        Me.TemaBotones = New Telerik.WinControls.Themes.AquaTheme()
        Me.icono = New System.Windows.Forms.PictureBox()
        Me.txtTexto = New System.Windows.Forms.Label()
        Me.Cancel_Button = New Telerik.WinControls.UI.RadButton()
        Me.OK_Button = New Telerik.WinControls.UI.RadButton()
        Me.chkLst = New System.Windows.Forms.CheckedListBox()
        CType(Me.icono, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Cancel_Button, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.OK_Button, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'icono
        '
        Me.icono.Location = New System.Drawing.Point(5, 9)
        Me.icono.Margin = New System.Windows.Forms.Padding(4)
        Me.icono.Name = "icono"
        Me.icono.Size = New System.Drawing.Size(57, 49)
        Me.icono.TabIndex = 5
        Me.icono.TabStop = False
        '
        'txtTexto
        '
        Me.txtTexto.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtTexto.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTexto.Location = New System.Drawing.Point(70, 9)
        Me.txtTexto.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.txtTexto.Name = "txtTexto"
        Me.txtTexto.Size = New System.Drawing.Size(437, 95)
        Me.txtTexto.TabIndex = 4
        Me.txtTexto.Text = "Label1"
        Me.txtTexto.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Cancel_Button
        '
        Me.Cancel_Button.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Cancel_Button.ForeColor = System.Drawing.Color.DarkRed
        Me.Cancel_Button.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.Cancel_Button.Location = New System.Drawing.Point(368, 323)
        Me.Cancel_Button.Name = "Cancel_Button"
        Me.Cancel_Button.Size = New System.Drawing.Size(107, 44)
        Me.Cancel_Button.TabIndex = 7
        Me.Cancel_Button.TabStop = False
        Me.Cancel_Button.Text = "&Cancelar"
        Me.Cancel_Button.ThemeName = "Aqua"
        '
        'OK_Button
        '
        Me.OK_Button.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OK_Button.ForeColor = System.Drawing.Color.MidnightBlue
        Me.OK_Button.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.OK_Button.Location = New System.Drawing.Point(186, 323)
        Me.OK_Button.Name = "OK_Button"
        Me.OK_Button.Size = New System.Drawing.Size(107, 44)
        Me.OK_Button.TabIndex = 6
        Me.OK_Button.TabStop = False
        Me.OK_Button.Text = "&Aceptar"
        Me.OK_Button.ThemeName = "Aqua"
        '
        'chkLst
        '
        Me.chkLst.FormattingEnabled = True
        Me.chkLst.Location = New System.Drawing.Point(61, 88)
        Me.chkLst.Margin = New System.Windows.Forms.Padding(4)
        Me.chkLst.Name = "chkLst"
        Me.chkLst.Size = New System.Drawing.Size(502, 196)
        Me.chkLst.TabIndex = 19
        '
        'DialogUnaSeleccion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(650, 379)
        Me.ControlBox = False
        Me.Controls.Add(Me.chkLst)
        Me.Controls.Add(Me.Cancel_Button)
        Me.Controls.Add(Me.OK_Button)
        Me.Controls.Add(Me.icono)
        Me.Controls.Add(Me.txtTexto)
        Me.Name = "DialogUnaSeleccion"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Selecciona una Opción"
        Me.ThemeName = "MaterialBlueGrey"
        CType(Me.icono, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Cancel_Button, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.OK_Button, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TemaGeneral As Telerik.WinControls.Themes.MaterialTheme
    Friend WithEvents TemaBotones As Telerik.WinControls.Themes.AquaTheme
    Friend WithEvents icono As PictureBox
    Friend WithEvents txtTexto As Label
    Friend WithEvents Cancel_Button As Telerik.WinControls.UI.RadButton
    Friend WithEvents OK_Button As Telerik.WinControls.UI.RadButton
    Friend WithEvents chkLst As CheckedListBox
End Class

