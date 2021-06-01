<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Importar_XML_DB
	Inherits Telerik.WinControls.UI.RadForm

	'Form overrides dispose to clean up the component list.
	<System.Diagnostics.DebuggerNonUserCode()>
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
	<System.Diagnostics.DebuggerStepThrough()>
	Private Sub InitializeComponent()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Importar_XML_DB))
        Me.TemaGeneral = New Telerik.WinControls.Themes.MaterialTheme()
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel()
        Me.lblRegistros = New System.Windows.Forms.Label()
        Me.CmdManual = New Telerik.WinControls.UI.RadButton()
        Me.RadRecibidas = New System.Windows.Forms.RadioButton()
        Me.RadEmitidas = New System.Windows.Forms.RadioButton()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Cmd_Procesar = New Telerik.WinControls.UI.RadButton()
        Me.CmdLimpiar = New Telerik.WinControls.UI.RadButton()
        Me.CmdImportar = New Telerik.WinControls.UI.RadButton()
        Me.cmdCerrar = New Telerik.WinControls.UI.RadButton()
        Me.TablaImportar = New System.Windows.Forms.DataGridView()
        Me.SP1 = New System.ComponentModel.BackgroundWorker()
        Me.SP2 = New System.ComponentModel.BackgroundWorker()
        Me.TemaBotones = New Telerik.WinControls.Themes.AquaTheme()
        Me.lstCliente = New ATMFiscal.Listas()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.CmdManual, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Cmd_Procesar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdLimpiar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdImportar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmdCerrar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TablaImportar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadPanel1
        '
        Me.RadPanel1.Controls.Add(Me.lblRegistros)
        Me.RadPanel1.Controls.Add(Me.CmdManual)
        Me.RadPanel1.Controls.Add(Me.RadRecibidas)
        Me.RadPanel1.Controls.Add(Me.RadEmitidas)
        Me.RadPanel1.Controls.Add(Me.lstCliente)
        Me.RadPanel1.Controls.Add(Me.Label3)
        Me.RadPanel1.Controls.Add(Me.Cmd_Procesar)
        Me.RadPanel1.Controls.Add(Me.CmdLimpiar)
        Me.RadPanel1.Controls.Add(Me.CmdImportar)
        Me.RadPanel1.Controls.Add(Me.cmdCerrar)
        Me.RadPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.RadPanel1.Location = New System.Drawing.Point(0, 0)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(1227, 82)
        Me.RadPanel1.TabIndex = 0
        '
        'lblRegistros
        '
        Me.lblRegistros.AutoSize = True
        Me.lblRegistros.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRegistros.Location = New System.Drawing.Point(889, 12)
        Me.lblRegistros.Name = "lblRegistros"
        Me.lblRegistros.Size = New System.Drawing.Size(158, 21)
        Me.lblRegistros.TabIndex = 123
        Me.lblRegistros.Text = "Total de registros:"
        '
        'CmdManual
        '
        Me.CmdManual.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CmdManual.Image = Global.ATMFiscal.My.Resources.Resources.Manualito
        Me.CmdManual.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdManual.Location = New System.Drawing.Point(1166, 13)
        Me.CmdManual.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdManual.Name = "CmdManual"
        Me.CmdManual.Size = New System.Drawing.Size(50, 54)
        Me.CmdManual.TabIndex = 121
        Me.CmdManual.TabStop = False
        Me.CmdManual.TextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.CmdManual.ThemeName = "Aqua"
        '
        'RadRecibidas
        '
        Me.RadRecibidas.AutoSize = True
        Me.RadRecibidas.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadRecibidas.ForeColor = System.Drawing.Color.DarkGreen
        Me.RadRecibidas.Location = New System.Drawing.Point(758, 49)
        Me.RadRecibidas.Name = "RadRecibidas"
        Me.RadRecibidas.Size = New System.Drawing.Size(111, 25)
        Me.RadRecibidas.TabIndex = 122
        Me.RadRecibidas.TabStop = True
        Me.RadRecibidas.Text = "Recibidas"
        Me.RadRecibidas.UseVisualStyleBackColor = True
        '
        'RadEmitidas
        '
        Me.RadEmitidas.AutoSize = True
        Me.RadEmitidas.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadEmitidas.ForeColor = System.Drawing.Color.Navy
        Me.RadEmitidas.Location = New System.Drawing.Point(758, 13)
        Me.RadEmitidas.Name = "RadEmitidas"
        Me.RadEmitidas.Size = New System.Drawing.Size(101, 25)
        Me.RadEmitidas.TabIndex = 100
        Me.RadEmitidas.TabStop = True
        Me.RadEmitidas.Text = "Emitidas"
        Me.RadEmitidas.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(247, 11)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 18)
        Me.Label3.TabIndex = 96
        Me.Label3.Text = "Empresa:"
        '
        'Cmd_Procesar
        '
        Me.Cmd_Procesar.Image = Global.ATMFiscal.My.Resources.Resources.Procesar
        Me.Cmd_Procesar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.Cmd_Procesar.Location = New System.Drawing.Point(173, 16)
        Me.Cmd_Procesar.Margin = New System.Windows.Forms.Padding(2)
        Me.Cmd_Procesar.Name = "Cmd_Procesar"
        Me.Cmd_Procesar.Size = New System.Drawing.Size(50, 54)
        Me.Cmd_Procesar.TabIndex = 120
        Me.Cmd_Procesar.TabStop = False
        Me.Cmd_Procesar.TextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.Cmd_Procesar.ThemeName = "Aqua"
        '
        'CmdLimpiar
        '
        Me.CmdLimpiar.Image = Global.ATMFiscal.My.Resources.Resources.LIMPIAR
        Me.CmdLimpiar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdLimpiar.Location = New System.Drawing.Point(65, 16)
        Me.CmdLimpiar.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdLimpiar.Name = "CmdLimpiar"
        Me.CmdLimpiar.Size = New System.Drawing.Size(50, 54)
        Me.CmdLimpiar.TabIndex = 120
        Me.CmdLimpiar.TabStop = False
        Me.CmdLimpiar.TextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.CmdLimpiar.ThemeName = "Aqua"
        '
        'CmdImportar
        '
        Me.CmdImportar.Image = Global.ATMFiscal.My.Resources.Resources.Importar_Datos
        Me.CmdImportar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdImportar.Location = New System.Drawing.Point(119, 16)
        Me.CmdImportar.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdImportar.Name = "CmdImportar"
        Me.CmdImportar.Size = New System.Drawing.Size(50, 54)
        Me.CmdImportar.TabIndex = 119
        Me.CmdImportar.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdImportar.ThemeName = "Aqua"
        '
        'cmdCerrar
        '
        Me.cmdCerrar.Image = Global.ATMFiscal.My.Resources.Resources.Salir
        Me.cmdCerrar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.cmdCerrar.Location = New System.Drawing.Point(11, 16)
        Me.cmdCerrar.Margin = New System.Windows.Forms.Padding(2)
        Me.cmdCerrar.Name = "cmdCerrar"
        Me.cmdCerrar.Size = New System.Drawing.Size(50, 54)
        Me.cmdCerrar.TabIndex = 119
        Me.cmdCerrar.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.cmdCerrar.ThemeName = "Aqua"
        '
        'TablaImportar
        '
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.TablaImportar.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.TablaImportar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer), CType(CType(32, Byte), Integer))
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.TablaImportar.DefaultCellStyle = DataGridViewCellStyle2
        Me.TablaImportar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TablaImportar.Location = New System.Drawing.Point(0, 82)
        Me.TablaImportar.Margin = New System.Windows.Forms.Padding(2)
        Me.TablaImportar.Name = "TablaImportar"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.TablaImportar.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.TablaImportar.RowTemplate.Height = 24
        Me.TablaImportar.Size = New System.Drawing.Size(1227, 382)
        Me.TablaImportar.TabIndex = 1
        '
        'SP1
        '
        Me.SP1.WorkerSupportsCancellation = True
        '
        'SP2
        '
        Me.SP2.WorkerSupportsCancellation = True
        '
        'lstCliente
        '
        Me.lstCliente.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstCliente.Location = New System.Drawing.Point(249, 35)
        Me.lstCliente.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lstCliente.Name = "lstCliente"
        Me.lstCliente.SelectItem = ""
        Me.lstCliente.SelectText = ""
        Me.lstCliente.Size = New System.Drawing.Size(494, 36)
        Me.lstCliente.TabIndex = 121
        '
        'Importar_XML_DB
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(1227, 464)
        Me.Controls.Add(Me.TablaImportar)
        Me.Controls.Add(Me.RadPanel1)
        Me.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Importar_XML_DB"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Tag = "P_Importar"
        Me.Text = "Importar Facturas y Notas de Credito"
        Me.ThemeName = "MaterialBlueGrey"
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        Me.RadPanel1.PerformLayout()
        CType(Me.CmdManual, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Cmd_Procesar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdLimpiar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdImportar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmdCerrar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TablaImportar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TemaGeneral As Telerik.WinControls.Themes.MaterialTheme
	Friend WithEvents RadPanel1 As Telerik.WinControls.UI.RadPanel
	Friend WithEvents Cmd_Procesar As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdLimpiar As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdImportar As Telerik.WinControls.UI.RadButton
	Friend WithEvents cmdCerrar As Telerik.WinControls.UI.RadButton
	Friend WithEvents Label3 As Label
	Friend WithEvents lstCliente As Listas
	Friend WithEvents RadEmitidas As RadioButton
	Friend WithEvents RadRecibidas As RadioButton
	Friend WithEvents CmdManual As Telerik.WinControls.UI.RadButton
	Friend WithEvents TablaImportar As DataGridView
	Friend WithEvents SP1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents SP2 As System.ComponentModel.BackgroundWorker
    Friend WithEvents TemaBotones As Telerik.WinControls.Themes.AquaTheme
    Friend WithEvents lblRegistros As Label
End Class

