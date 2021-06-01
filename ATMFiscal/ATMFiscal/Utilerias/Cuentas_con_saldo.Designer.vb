<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Cuentas_con_saldo
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
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel()
        Me.Cmd_Guardar = New Telerik.WinControls.UI.RadButton()
        Me.Barra = New System.Windows.Forms.ProgressBar()
        Me.ComboAñoB = New Telerik.WinControls.UI.RadDropDownList()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cmdCerrar = New Telerik.WinControls.UI.RadButton()
        Me.SegundoPlano = New System.ComponentModel.BackgroundWorker()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.Cmd_Guardar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ComboAñoB, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmdCerrar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadPanel1
        '
        Me.RadPanel1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.RadPanel1.Controls.Add(Me.Cmd_Guardar)
        Me.RadPanel1.Controls.Add(Me.Barra)
        Me.RadPanel1.Controls.Add(Me.ComboAñoB)
        Me.RadPanel1.Controls.Add(Me.Label10)
        Me.RadPanel1.Controls.Add(Me.cmdCerrar)
        Me.RadPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.RadPanel1.Location = New System.Drawing.Point(0, 0)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(282, 125)
        Me.RadPanel1.TabIndex = 0
        '
        'Cmd_Guardar
        '
        Me.Cmd_Guardar.Image = Global.ATMFiscal.My.Resources.Resources.Procesar
        Me.Cmd_Guardar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.Cmd_Guardar.Location = New System.Drawing.Point(65, 11)
        Me.Cmd_Guardar.Margin = New System.Windows.Forms.Padding(2)
        Me.Cmd_Guardar.Name = "Cmd_Guardar"
        Me.Cmd_Guardar.Size = New System.Drawing.Size(50, 54)
        Me.Cmd_Guardar.TabIndex = 660
        Me.Cmd_Guardar.TabStop = False
        Me.Cmd_Guardar.TextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.Cmd_Guardar.ThemeName = "Aqua"
        '
        'Barra
        '
        Me.Barra.Location = New System.Drawing.Point(11, 72)
        Me.Barra.Margin = New System.Windows.Forms.Padding(4)
        Me.Barra.Name = "Barra"
        Me.Barra.Size = New System.Drawing.Size(262, 36)
        Me.Barra.TabIndex = 659
        '
        'ComboAñoB
        '
        Me.ComboAñoB.Location = New System.Drawing.Point(130, 24)
        Me.ComboAñoB.Name = "ComboAñoB"
        Me.ComboAñoB.Size = New System.Drawing.Size(143, 36)
        Me.ComboAñoB.TabIndex = 587
        Me.ComboAñoB.Text = " "
        Me.ComboAñoB.ThemeName = "Material"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(127, 3)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(38, 18)
        Me.Label10.TabIndex = 586
        Me.Label10.Text = "Año:"
        '
        'cmdCerrar
        '
        Me.cmdCerrar.Image = Global.ATMFiscal.My.Resources.Resources.Salir
        Me.cmdCerrar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.cmdCerrar.Location = New System.Drawing.Point(11, 11)
        Me.cmdCerrar.Margin = New System.Windows.Forms.Padding(2)
        Me.cmdCerrar.Name = "cmdCerrar"
        Me.cmdCerrar.Size = New System.Drawing.Size(50, 54)
        Me.cmdCerrar.TabIndex = 585
        Me.cmdCerrar.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.cmdCerrar.ThemeName = "Aqua"
        '
        'SegundoPlano
        '
        Me.SegundoPlano.WorkerSupportsCancellation = True
        '
        'Cuentas_con_saldo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(282, 128)
        Me.ControlBox = False
        Me.Controls.Add(Me.RadPanel1)
        Me.Name = "Cuentas_con_saldo"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cuentas con Saldo"
        Me.ThemeName = "MaterialBlueGrey"
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        Me.RadPanel1.PerformLayout()
        CType(Me.Cmd_Guardar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ComboAñoB, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmdCerrar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents RadPanel1 As Telerik.WinControls.UI.RadPanel
    Friend WithEvents cmdCerrar As Telerik.WinControls.UI.RadButton
    Friend WithEvents ComboAñoB As Telerik.WinControls.UI.RadDropDownList
    Friend WithEvents Label10 As Label
    Friend WithEvents SegundoPlano As System.ComponentModel.BackgroundWorker
    Friend WithEvents Barra As ProgressBar
    Friend WithEvents Cmd_Guardar As Telerik.WinControls.UI.RadButton
End Class

