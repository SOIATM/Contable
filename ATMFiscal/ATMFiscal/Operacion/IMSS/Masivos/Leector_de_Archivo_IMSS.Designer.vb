<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Leector_de_Archivo_IMSS
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
		Me.components = New System.ComponentModel.Container()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Leector_de_Archivo_IMSS))
		Me.TemaGeneral = New Telerik.WinControls.Themes.MaterialTheme()
		Me.TemaBotones = New Telerik.WinControls.Themes.AquaTheme()
		Me.Ayuda = New System.Windows.Forms.ToolTip(Me.components)
		Me.DialogoImpresoras = New System.Windows.Forms.PrintDialog()
		Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel()
		Me.lstCliente = New ATMFiscal.Listas()
		Me.Label3 = New System.Windows.Forms.Label()
		Me.cmdCerrar = New Telerik.WinControls.UI.RadButton()
		Me.CmdBuscarN = New Telerik.WinControls.UI.RadButton()
		Me.Cmd_Procesar = New Telerik.WinControls.UI.RadButton()
		Me.CmdLimpiarN = New Telerik.WinControls.UI.RadButton()
		Me.TablaAltas = New System.Windows.Forms.DataGridView()
		CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.RadPanel1.SuspendLayout()
		CType(Me.cmdCerrar, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.CmdBuscarN, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.Cmd_Procesar, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.CmdLimpiarN, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me.TablaAltas, System.ComponentModel.ISupportInitialize).BeginInit()
		CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
		Me.SuspendLayout()
		'
		'Ayuda
		'
		Me.Ayuda.IsBalloon = True
		'
		'DialogoImpresoras
		'
		Me.DialogoImpresoras.UseEXDialog = True
		'
		'RadPanel1
		'
		Me.RadPanel1.Controls.Add(Me.lstCliente)
		Me.RadPanel1.Controls.Add(Me.Label3)
		Me.RadPanel1.Controls.Add(Me.cmdCerrar)
		Me.RadPanel1.Controls.Add(Me.CmdBuscarN)
		Me.RadPanel1.Controls.Add(Me.Cmd_Procesar)
		Me.RadPanel1.Controls.Add(Me.CmdLimpiarN)
		Me.RadPanel1.Dock = System.Windows.Forms.DockStyle.Top
		Me.RadPanel1.Location = New System.Drawing.Point(0, 0)
		Me.RadPanel1.Name = "RadPanel1"
		Me.RadPanel1.Size = New System.Drawing.Size(801, 79)
		Me.RadPanel1.TabIndex = 0
		'
		'lstCliente
		'
		Me.lstCliente.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.lstCliente.Location = New System.Drawing.Point(240, 33)
		Me.lstCliente.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
		Me.lstCliente.Name = "lstCliente"
		Me.lstCliente.SelectItem = ""
		Me.lstCliente.SelectText = ""
		Me.lstCliente.Size = New System.Drawing.Size(370, 36)
		Me.lstCliente.TabIndex = 626
		'
		'Label3
		'
		Me.Label3.AutoSize = True
		Me.Label3.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Label3.Location = New System.Drawing.Point(236, 13)
		Me.Label3.Name = "Label3"
		Me.Label3.Size = New System.Drawing.Size(76, 17)
		Me.Label3.TabIndex = 625
		Me.Label3.Text = "Empresa:"
		'
		'cmdCerrar
		'
		Me.cmdCerrar.Image = Global.ATMFiscal.My.Resources.Resources.Salir
		Me.cmdCerrar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
		Me.cmdCerrar.Location = New System.Drawing.Point(11, 11)
		Me.cmdCerrar.Margin = New System.Windows.Forms.Padding(2)
		Me.cmdCerrar.Name = "cmdCerrar"
		Me.cmdCerrar.Size = New System.Drawing.Size(50, 54)
		Me.cmdCerrar.TabIndex = 623
		Me.cmdCerrar.TextAlignment = System.Drawing.ContentAlignment.BottomRight
		Me.cmdCerrar.ThemeName = "Aqua"
		'
		'CmdBuscarN
		'
		Me.CmdBuscarN.Image = Global.ATMFiscal.My.Resources.Resources.Importar_Datos
		Me.CmdBuscarN.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
		Me.CmdBuscarN.Location = New System.Drawing.Point(119, 11)
		Me.CmdBuscarN.Margin = New System.Windows.Forms.Padding(2)
		Me.CmdBuscarN.Name = "CmdBuscarN"
		Me.CmdBuscarN.Size = New System.Drawing.Size(50, 54)
		Me.CmdBuscarN.TabIndex = 621
		Me.CmdBuscarN.TabStop = False
		Me.CmdBuscarN.TextAlignment = System.Drawing.ContentAlignment.TopRight
		Me.CmdBuscarN.ThemeName = "Aqua"
		'
		'Cmd_Procesar
		'
		Me.Cmd_Procesar.Image = Global.ATMFiscal.My.Resources.Resources.Procesar
		Me.Cmd_Procesar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
		Me.Cmd_Procesar.Location = New System.Drawing.Point(173, 11)
		Me.Cmd_Procesar.Margin = New System.Windows.Forms.Padding(2)
		Me.Cmd_Procesar.Name = "Cmd_Procesar"
		Me.Cmd_Procesar.Size = New System.Drawing.Size(50, 54)
		Me.Cmd_Procesar.TabIndex = 624
		Me.Cmd_Procesar.TextAlignment = System.Drawing.ContentAlignment.BottomRight
		Me.Cmd_Procesar.ThemeName = "Aqua"
		'
		'CmdLimpiarN
		'
		Me.CmdLimpiarN.Image = Global.ATMFiscal.My.Resources.Resources.LIMPIAR
		Me.CmdLimpiarN.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
		Me.CmdLimpiarN.Location = New System.Drawing.Point(65, 11)
		Me.CmdLimpiarN.Margin = New System.Windows.Forms.Padding(2)
		Me.CmdLimpiarN.Name = "CmdLimpiarN"
		Me.CmdLimpiarN.Size = New System.Drawing.Size(50, 54)
		Me.CmdLimpiarN.TabIndex = 622
		Me.CmdLimpiarN.TabStop = False
		Me.CmdLimpiarN.TextAlignment = System.Drawing.ContentAlignment.TopRight
		Me.CmdLimpiarN.ThemeName = "Aqua"
		'
		'TablaAltas
		'
		Me.TablaAltas.AllowUserToAddRows = False
		Me.TablaAltas.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders
		Me.TablaAltas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
		Me.TablaAltas.Dock = System.Windows.Forms.DockStyle.Fill
		Me.TablaAltas.Location = New System.Drawing.Point(0, 79)
		Me.TablaAltas.Margin = New System.Windows.Forms.Padding(2)
		Me.TablaAltas.Name = "TablaAltas"
		Me.TablaAltas.RowTemplate.Height = 24
		Me.TablaAltas.Size = New System.Drawing.Size(801, 267)
		Me.TablaAltas.TabIndex = 5
		'
		'Leector_de_Archivo_IMSS
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.BackColor = System.Drawing.Color.LightSteelBlue
		Me.ClientSize = New System.Drawing.Size(801, 346)
		Me.Controls.Add(Me.TablaAltas)
		Me.Controls.Add(Me.RadPanel1)
		Me.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
		Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
		Me.Name = "Leector_de_Archivo_IMSS"
		'
		'
		'
		Me.RootElement.ApplyShapeToControl = True
		Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
		Me.Text = "Leector de Archivo IMSS"
		Me.ThemeName = "Material"
		CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
		Me.RadPanel1.ResumeLayout(False)
		Me.RadPanel1.PerformLayout()
		CType(Me.cmdCerrar, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.CmdBuscarN, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.Cmd_Procesar, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.CmdLimpiarN, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me.TablaAltas, System.ComponentModel.ISupportInitialize).EndInit()
		CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
		Me.ResumeLayout(False)

	End Sub

	Friend WithEvents TemaGeneral As Telerik.WinControls.Themes.MaterialTheme
	Friend WithEvents TemaBotones As Telerik.WinControls.Themes.AquaTheme
	Friend WithEvents Ayuda As ToolTip
	Friend WithEvents DialogoImpresoras As PrintDialog
	Friend WithEvents RadPanel1 As Telerik.WinControls.UI.RadPanel
	Friend WithEvents lstCliente As Listas
	Friend WithEvents Label3 As Label
	Friend WithEvents cmdCerrar As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdBuscarN As Telerik.WinControls.UI.RadButton
	Friend WithEvents Cmd_Procesar As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdLimpiarN As Telerik.WinControls.UI.RadButton
	Friend WithEvents TablaAltas As DataGridView
End Class

