<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Importar_Complementos
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Importar_Complementos))
        Me.RadPanel2 = New Telerik.WinControls.UI.RadPanel()
        Me.CmdManual = New Telerik.WinControls.UI.RadButton()
        Me.lblRegistros = New System.Windows.Forms.Label()
        Me.RadRecibidas = New System.Windows.Forms.RadioButton()
        Me.RadEmitidas = New System.Windows.Forms.RadioButton()
        Me.lstCliente = New ATMFiscal.Listas()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Cmd_Procesar = New Telerik.WinControls.UI.RadButton()
        Me.CmdLimpiar = New Telerik.WinControls.UI.RadButton()
        Me.CmdImportar = New Telerik.WinControls.UI.RadButton()
        Me.cmdCerrar = New Telerik.WinControls.UI.RadButton()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.SP1 = New System.ComponentModel.BackgroundWorker()
        Me.SP2 = New System.ComponentModel.BackgroundWorker()
        Me.TemaGeneral = New Telerik.WinControls.Themes.MaterialTheme()
        Me.TablaImportar = New System.Windows.Forms.DataGridView()
        Me.TemaBotones = New Telerik.WinControls.Themes.AquaTheme()
        CType(Me.RadPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel2.SuspendLayout()
        CType(Me.CmdManual, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Cmd_Procesar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdLimpiar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdImportar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmdCerrar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TablaImportar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadPanel2
        '
        Me.RadPanel2.Controls.Add(Me.CmdManual)
        Me.RadPanel2.Controls.Add(Me.lblRegistros)
        Me.RadPanel2.Controls.Add(Me.RadRecibidas)
        Me.RadPanel2.Controls.Add(Me.RadEmitidas)
        Me.RadPanel2.Controls.Add(Me.lstCliente)
        Me.RadPanel2.Controls.Add(Me.Label3)
        Me.RadPanel2.Controls.Add(Me.Cmd_Procesar)
        Me.RadPanel2.Controls.Add(Me.CmdLimpiar)
        Me.RadPanel2.Controls.Add(Me.CmdImportar)
        Me.RadPanel2.Controls.Add(Me.cmdCerrar)
        Me.RadPanel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.RadPanel2.Location = New System.Drawing.Point(0, 0)
        Me.RadPanel2.Name = "RadPanel2"
        Me.RadPanel2.Size = New System.Drawing.Size(1244, 82)
        Me.RadPanel2.TabIndex = 1
        '
        'CmdManual
        '
        Me.CmdManual.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CmdManual.Image = Global.ATMFiscal.My.Resources.Resources.Manualito
        Me.CmdManual.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdManual.Location = New System.Drawing.Point(1180, 12)
        Me.CmdManual.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdManual.Name = "CmdManual"
        Me.CmdManual.Size = New System.Drawing.Size(50, 54)
        Me.CmdManual.TabIndex = 122
        Me.CmdManual.TabStop = False
        Me.CmdManual.TextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.CmdManual.ThemeName = "Aqua"
        '
        'lblRegistros
        '
        Me.lblRegistros.AutoSize = True
        Me.lblRegistros.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRegistros.Location = New System.Drawing.Point(869, 16)
        Me.lblRegistros.Name = "lblRegistros"
        Me.lblRegistros.Size = New System.Drawing.Size(158, 21)
        Me.lblRegistros.TabIndex = 21
        Me.lblRegistros.Text = "Total de registros:"
        '
        'RadRecibidas
        '
        Me.RadRecibidas.AutoSize = True
        Me.RadRecibidas.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadRecibidas.ForeColor = System.Drawing.Color.DarkGreen
        Me.RadRecibidas.Location = New System.Drawing.Point(717, 46)
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
        Me.RadEmitidas.Location = New System.Drawing.Point(717, 12)
        Me.RadEmitidas.Name = "RadEmitidas"
        Me.RadEmitidas.Size = New System.Drawing.Size(101, 25)
        Me.RadEmitidas.TabIndex = 100
        Me.RadEmitidas.TabStop = True
        Me.RadEmitidas.Text = "Emitidas"
        Me.RadEmitidas.UseVisualStyleBackColor = True
        '
        'lstCliente
        '
        Me.lstCliente.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstCliente.Location = New System.Drawing.Point(240, 36)
        Me.lstCliente.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lstCliente.Name = "lstCliente"
        Me.lstCliente.SelectItem = ""
        Me.lstCliente.SelectText = ""
        Me.lstCliente.Size = New System.Drawing.Size(452, 36)
        Me.lstCliente.TabIndex = 121
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
        Me.Cmd_Procesar.Location = New System.Drawing.Point(170, 15)
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
        Me.CmdLimpiar.Location = New System.Drawing.Point(62, 15)
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
        Me.CmdImportar.Location = New System.Drawing.Point(116, 15)
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
        Me.cmdCerrar.Location = New System.Drawing.Point(8, 15)
        Me.cmdCerrar.Margin = New System.Windows.Forms.Padding(2)
        Me.cmdCerrar.Name = "cmdCerrar"
        Me.cmdCerrar.Size = New System.Drawing.Size(50, 54)
        Me.cmdCerrar.TabIndex = 119
        Me.cmdCerrar.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.cmdCerrar.ThemeName = "Aqua"
        '
        'ToolTip1
        '
        Me.ToolTip1.IsBalloon = True
        Me.ToolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        '
        'SP1
        '
        Me.SP1.WorkerSupportsCancellation = True
        '
        'SP2
        '
        Me.SP2.WorkerSupportsCancellation = True
        '
        'TablaImportar
        '
        Me.TablaImportar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.TablaImportar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TablaImportar.Location = New System.Drawing.Point(0, 82)
        Me.TablaImportar.Margin = New System.Windows.Forms.Padding(2)
        Me.TablaImportar.Name = "TablaImportar"
        Me.TablaImportar.RowTemplate.Height = 24
        Me.TablaImportar.Size = New System.Drawing.Size(1244, 429)
        Me.TablaImportar.TabIndex = 2
        '
        'Importar_Complementos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(1244, 511)
        Me.Controls.Add(Me.TablaImportar)
        Me.Controls.Add(Me.RadPanel2)
        Me.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Importar_Complementos"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Tag = "P_Importar"
        Me.Text = "Importar Complementos"
        Me.ThemeName = "MaterialBlueGrey"
        CType(Me.RadPanel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel2.ResumeLayout(False)
        Me.RadPanel2.PerformLayout()
        CType(Me.CmdManual, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Cmd_Procesar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdLimpiar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdImportar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmdCerrar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TablaImportar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents RadPanel2 As Telerik.WinControls.UI.RadPanel
    Friend WithEvents RadRecibidas As RadioButton
    Friend WithEvents RadEmitidas As RadioButton
    Friend WithEvents lstCliente As Listas
    Friend WithEvents Label3 As Label
    Friend WithEvents Cmd_Procesar As Telerik.WinControls.UI.RadButton
    Friend WithEvents CmdLimpiar As Telerik.WinControls.UI.RadButton
    Friend WithEvents CmdImportar As Telerik.WinControls.UI.RadButton
    Friend WithEvents cmdCerrar As Telerik.WinControls.UI.RadButton
    Friend WithEvents ToolTip1 As ToolTip
    Friend WithEvents SP1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents SP2 As System.ComponentModel.BackgroundWorker
    Friend WithEvents TemaGeneral As Telerik.WinControls.Themes.MaterialTheme
    Friend WithEvents lblRegistros As Label
    Friend WithEvents CmdManual As Telerik.WinControls.UI.RadButton
	Friend WithEvents TablaImportar As DataGridView
	Friend WithEvents TemaBotones As Telerik.WinControls.Themes.AquaTheme
End Class

