<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EliminaXML
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(EliminaXML))
        Me.TemaGeneral = New Telerik.WinControls.Themes.MaterialTheme()
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.LstCFDI = New ATMFiscal.Listas()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lstCliente = New ATMFiscal.Listas()
        Me.CmdLimpiar = New Telerik.WinControls.UI.RadButton()
        Me.CmdImportar = New Telerik.WinControls.UI.RadButton()
        Me.Cmd_Procesar = New Telerik.WinControls.UI.RadButton()
        Me.cmdCerrar = New Telerik.WinControls.UI.RadButton()
        Me.CmdManual = New Telerik.WinControls.UI.RadButton()
        Me.TablaImportar = New System.Windows.Forms.DataGridView()
        Me.Verif = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.UUI = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Imp = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Ref = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Id = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Ayuda = New System.Windows.Forms.ToolTip(Me.components)
        Me.TemaBotones = New Telerik.WinControls.Themes.AquaTheme()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.CmdLimpiar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdImportar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Cmd_Procesar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmdCerrar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdManual, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TablaImportar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadPanel1
        '
        Me.RadPanel1.Controls.Add(Me.Label1)
        Me.RadPanel1.Controls.Add(Me.LstCFDI)
        Me.RadPanel1.Controls.Add(Me.Label2)
        Me.RadPanel1.Controls.Add(Me.lstCliente)
        Me.RadPanel1.Controls.Add(Me.CmdLimpiar)
        Me.RadPanel1.Controls.Add(Me.CmdImportar)
        Me.RadPanel1.Controls.Add(Me.Cmd_Procesar)
        Me.RadPanel1.Controls.Add(Me.cmdCerrar)
        Me.RadPanel1.Controls.Add(Me.CmdManual)
        Me.RadPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.RadPanel1.Location = New System.Drawing.Point(0, 0)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(813, 137)
        Me.RadPanel1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(279, 71)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(45, 18)
        Me.Label1.TabIndex = 548
        Me.Label1.Text = "UUID:"
        '
        'LstCFDI
        '
        Me.LstCFDI.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LstCFDI.Location = New System.Drawing.Point(283, 95)
        Me.LstCFDI.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LstCFDI.Name = "LstCFDI"
        Me.LstCFDI.SelectItem = ""
        Me.LstCFDI.SelectText = ""
        Me.LstCFDI.Size = New System.Drawing.Size(423, 36)
        Me.LstCFDI.TabIndex = 549
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(279, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 18)
        Me.Label2.TabIndex = 546
        Me.Label2.Text = "Empresa:"
        '
        'lstCliente
        '
        Me.lstCliente.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstCliente.Location = New System.Drawing.Point(283, 33)
        Me.lstCliente.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lstCliente.Name = "lstCliente"
        Me.lstCliente.SelectItem = ""
        Me.lstCliente.SelectText = ""
        Me.lstCliente.Size = New System.Drawing.Size(423, 36)
        Me.lstCliente.TabIndex = 547
        '
        'CmdLimpiar
        '
        Me.CmdLimpiar.Image = Global.ATMFiscal.My.Resources.Resources.LIMPIAR
        Me.CmdLimpiar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdLimpiar.Location = New System.Drawing.Point(52, 15)
        Me.CmdLimpiar.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdLimpiar.Name = "CmdLimpiar"
        Me.CmdLimpiar.Size = New System.Drawing.Size(50, 54)
        Me.CmdLimpiar.TabIndex = 545
        Me.CmdLimpiar.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdLimpiar.ThemeName = "Aqua"
        '
        'CmdImportar
        '
        Me.CmdImportar.Image = Global.ATMFiscal.My.Resources.Resources.Buscar
        Me.CmdImportar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdImportar.Location = New System.Drawing.Point(105, 15)
        Me.CmdImportar.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdImportar.Name = "CmdImportar"
        Me.CmdImportar.Size = New System.Drawing.Size(50, 54)
        Me.CmdImportar.TabIndex = 542
        Me.CmdImportar.TabStop = False
        Me.CmdImportar.TextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.CmdImportar.ThemeName = "Aqua"
        '
        'Cmd_Procesar
        '
        Me.Cmd_Procesar.Image = Global.ATMFiscal.My.Resources.Resources.Procesar
        Me.Cmd_Procesar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.Cmd_Procesar.Location = New System.Drawing.Point(158, 15)
        Me.Cmd_Procesar.Margin = New System.Windows.Forms.Padding(2)
        Me.Cmd_Procesar.Name = "Cmd_Procesar"
        Me.Cmd_Procesar.Size = New System.Drawing.Size(50, 54)
        Me.Cmd_Procesar.TabIndex = 543
        Me.Cmd_Procesar.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.Cmd_Procesar.ThemeName = "Aqua"
        '
        'cmdCerrar
        '
        Me.cmdCerrar.Image = CType(resources.GetObject("cmdCerrar.Image"), System.Drawing.Image)
        Me.cmdCerrar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.cmdCerrar.Location = New System.Drawing.Point(-1, 15)
        Me.cmdCerrar.Margin = New System.Windows.Forms.Padding(2)
        Me.cmdCerrar.Name = "cmdCerrar"
        Me.cmdCerrar.Size = New System.Drawing.Size(50, 54)
        Me.cmdCerrar.TabIndex = 541
        Me.cmdCerrar.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.cmdCerrar.ThemeName = "Aqua"
        '
        'CmdManual
        '
        Me.CmdManual.Image = Global.ATMFiscal.My.Resources.Resources.Manualito
        Me.CmdManual.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdManual.Location = New System.Drawing.Point(212, 15)
        Me.CmdManual.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdManual.Name = "CmdManual"
        Me.CmdManual.Size = New System.Drawing.Size(50, 54)
        Me.CmdManual.TabIndex = 544
        Me.CmdManual.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdManual.ThemeName = "Aqua"
        '
        'TablaImportar
        '
        Me.TablaImportar.AllowUserToAddRows = False
        Me.TablaImportar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.TablaImportar.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Verif, Me.UUI, Me.Imp, Me.Ref, Me.Id})
        Me.TablaImportar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TablaImportar.Location = New System.Drawing.Point(0, 137)
        Me.TablaImportar.Margin = New System.Windows.Forms.Padding(2)
        Me.TablaImportar.Name = "TablaImportar"
        Me.TablaImportar.RowTemplate.Height = 24
        Me.TablaImportar.Size = New System.Drawing.Size(813, 117)
        Me.TablaImportar.TabIndex = 3
        '
        'Verif
        '
        Me.Verif.HeaderText = "Lista"
        Me.Verif.Name = "Verif"
        '
        'UUI
        '
        Me.UUI.HeaderText = "UUID"
        Me.UUI.Name = "UUI"
        '
        'Imp
        '
        Me.Imp.HeaderText = "Importe"
        Me.Imp.Name = "Imp"
        '
        'Ref
        '
        Me.Ref.HeaderText = "Referencia"
        Me.Ref.Name = "Ref"
        '
        'Id
        '
        Me.Id.HeaderText = "IDS"
        Me.Id.Name = "Id"
        Me.Id.Visible = False
        '
        'Ayuda
        '
        Me.Ayuda.IsBalloon = True
        Me.Ayuda.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info
        '
        'EliminaXML
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(813, 254)
        Me.ControlBox = False
        Me.Controls.Add(Me.TablaImportar)
        Me.Controls.Add(Me.RadPanel1)
        Me.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "EliminaXML"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Tag = "P_Master"
        Me.Text = "Elimina XML"
        Me.ThemeName = "MaterialBlueGrey"
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        Me.RadPanel1.PerformLayout()
        CType(Me.CmdLimpiar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdImportar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Cmd_Procesar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmdCerrar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdManual, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TablaImportar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TemaGeneral As Telerik.WinControls.Themes.MaterialTheme
	Friend WithEvents RadPanel1 As Telerik.WinControls.UI.RadPanel
	Friend WithEvents CmdLimpiar As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdImportar As Telerik.WinControls.UI.RadButton
	Friend WithEvents Cmd_Procesar As Telerik.WinControls.UI.RadButton
	Friend WithEvents cmdCerrar As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdManual As Telerik.WinControls.UI.RadButton
	Friend WithEvents Label2 As Label
	Friend WithEvents lstCliente As Listas
	Friend WithEvents Label1 As Label
	Friend WithEvents LstCFDI As Listas
	Friend WithEvents TablaImportar As DataGridView
	Friend WithEvents Verif As DataGridViewCheckBoxColumn
	Friend WithEvents UUI As DataGridViewTextBoxColumn
	Friend WithEvents Imp As DataGridViewTextBoxColumn
	Friend WithEvents Ref As DataGridViewTextBoxColumn
	Friend WithEvents Id As DataGridViewTextBoxColumn
	Friend WithEvents Ayuda As ToolTip
	Friend WithEvents TemaBotones As Telerik.WinControls.Themes.AquaTheme
End Class

