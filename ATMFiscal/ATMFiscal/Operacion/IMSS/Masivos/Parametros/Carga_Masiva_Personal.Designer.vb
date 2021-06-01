<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Carga_Masiva_Personal
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Carga_Masiva_Personal))
        Me.Ayuda = New System.Windows.Forms.ToolTip(Me.components)
        Me.TemaGeneral = New Telerik.WinControls.Themes.MaterialTheme()
        Me.TemaBotones = New Telerik.WinControls.Themes.AquaTheme()
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel()
        Me.lblRegistros = New System.Windows.Forms.Label()
        Me.LstTexto = New ATMFiscal.Listas()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.LstGuia = New ATMFiscal.Listas()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.LstSubDelegacion = New ATMFiscal.Listas()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.LstDelegacion = New ATMFiscal.Listas()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lstCliente = New ATMFiscal.Listas()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.CmdPlantlla = New Telerik.WinControls.UI.RadButton()
        Me.Cmd_Procesar = New Telerik.WinControls.UI.RadButton()
        Me.CmdAsignaDelegacion = New Telerik.WinControls.UI.RadButton()
        Me.CmdBuscarN = New Telerik.WinControls.UI.RadButton()
        Me.cmdCerrar = New Telerik.WinControls.UI.RadButton()
        Me.CmdLimpiarN = New Telerik.WinControls.UI.RadButton()
        Me.RadPanel2 = New Telerik.WinControls.UI.RadPanel()
        Me.Tabla = New System.Windows.Forms.DataGridView()
        Me.Val = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Des = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TablaImportar = New System.Windows.Forms.DataGridView()
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.CmdPlantlla, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Cmd_Procesar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdAsignaDelegacion, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdBuscarN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmdCerrar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdLimpiarN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadPanel2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel2.SuspendLayout()
        CType(Me.Tabla, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TablaImportar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Ayuda
        '
        Me.Ayuda.IsBalloon = True
        '
        'RadPanel1
        '
        Me.RadPanel1.BackColor = System.Drawing.Color.CadetBlue
        Me.RadPanel1.Controls.Add(Me.lblRegistros)
        Me.RadPanel1.Controls.Add(Me.LstTexto)
        Me.RadPanel1.Controls.Add(Me.Label4)
        Me.RadPanel1.Controls.Add(Me.LstGuia)
        Me.RadPanel1.Controls.Add(Me.Label3)
        Me.RadPanel1.Controls.Add(Me.LstSubDelegacion)
        Me.RadPanel1.Controls.Add(Me.Label2)
        Me.RadPanel1.Controls.Add(Me.LstDelegacion)
        Me.RadPanel1.Controls.Add(Me.Label1)
        Me.RadPanel1.Controls.Add(Me.lstCliente)
        Me.RadPanel1.Controls.Add(Me.Label6)
        Me.RadPanel1.Controls.Add(Me.CmdPlantlla)
        Me.RadPanel1.Controls.Add(Me.Cmd_Procesar)
        Me.RadPanel1.Controls.Add(Me.CmdAsignaDelegacion)
        Me.RadPanel1.Controls.Add(Me.CmdBuscarN)
        Me.RadPanel1.Controls.Add(Me.cmdCerrar)
        Me.RadPanel1.Controls.Add(Me.CmdLimpiarN)
        Me.RadPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.RadPanel1.Location = New System.Drawing.Point(0, 0)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(1041, 224)
        Me.RadPanel1.TabIndex = 0
        '
        'lblRegistros
        '
        Me.lblRegistros.AutoSize = True
        Me.lblRegistros.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRegistros.Location = New System.Drawing.Point(21, 196)
        Me.lblRegistros.Name = "lblRegistros"
        Me.lblRegistros.Size = New System.Drawing.Size(144, 21)
        Me.lblRegistros.TabIndex = 692
        Me.lblRegistros.Text = "Total de Registros:"
        '
        'LstTexto
        '
        Me.LstTexto.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LstTexto.Location = New System.Drawing.Point(19, 146)
        Me.LstTexto.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LstTexto.Name = "LstTexto"
        Me.LstTexto.SelectItem = ""
        Me.LstTexto.SelectText = ""
        Me.LstTexto.Size = New System.Drawing.Size(291, 36)
        Me.LstTexto.TabIndex = 611
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(16, 127)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(119, 18)
        Me.Label4.TabIndex = 610
        Me.Label4.Text = "Valor a Asignar:"
        '
        'LstGuia
        '
        Me.LstGuia.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LstGuia.Location = New System.Drawing.Point(20, 87)
        Me.LstGuia.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LstGuia.Name = "LstGuia"
        Me.LstGuia.SelectItem = ""
        Me.LstGuia.SelectText = ""
        Me.LstGuia.Size = New System.Drawing.Size(290, 36)
        Me.LstGuia.TabIndex = 609
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(17, 68)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(44, 18)
        Me.Label3.TabIndex = 608
        Me.Label3.Text = "Guia:"
        '
        'LstSubDelegacion
        '
        Me.LstSubDelegacion.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LstSubDelegacion.Location = New System.Drawing.Point(346, 146)
        Me.LstSubDelegacion.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LstSubDelegacion.Name = "LstSubDelegacion"
        Me.LstSubDelegacion.SelectItem = ""
        Me.LstSubDelegacion.SelectText = ""
        Me.LstSubDelegacion.Size = New System.Drawing.Size(370, 36)
        Me.LstSubDelegacion.TabIndex = 607
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(343, 127)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(119, 18)
        Me.Label2.TabIndex = 606
        Me.Label2.Text = "Sub Delegacion:"
        '
        'LstDelegacion
        '
        Me.LstDelegacion.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LstDelegacion.Location = New System.Drawing.Point(346, 87)
        Me.LstDelegacion.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.LstDelegacion.Name = "LstDelegacion"
        Me.LstDelegacion.SelectItem = ""
        Me.LstDelegacion.SelectText = ""
        Me.LstDelegacion.Size = New System.Drawing.Size(370, 36)
        Me.LstDelegacion.TabIndex = 605
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(343, 68)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(88, 18)
        Me.Label1.TabIndex = 604
        Me.Label1.Text = "Delegacion:"
        '
        'lstCliente
        '
        Me.lstCliente.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstCliente.Location = New System.Drawing.Point(346, 28)
        Me.lstCliente.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lstCliente.Name = "lstCliente"
        Me.lstCliente.SelectItem = ""
        Me.lstCliente.SelectText = ""
        Me.lstCliente.Size = New System.Drawing.Size(370, 36)
        Me.lstCliente.TabIndex = 603
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(343, 9)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(72, 18)
        Me.Label6.TabIndex = 602
        Me.Label6.Text = "Empresa:"
        '
        'CmdPlantlla
        '
        Me.CmdPlantlla.Image = Global.ATMFiscal.My.Resources.Resources.documentos
        Me.CmdPlantlla.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdPlantlla.Location = New System.Drawing.Point(275, 2)
        Me.CmdPlantlla.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdPlantlla.Name = "CmdPlantlla"
        Me.CmdPlantlla.Size = New System.Drawing.Size(50, 54)
        Me.CmdPlantlla.TabIndex = 600
        Me.CmdPlantlla.TabStop = False
        Me.CmdPlantlla.TextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.CmdPlantlla.ThemeName = "Aqua"
        '
        'Cmd_Procesar
        '
        Me.Cmd_Procesar.Image = Global.ATMFiscal.My.Resources.Resources.Procesar
        Me.Cmd_Procesar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.Cmd_Procesar.Location = New System.Drawing.Point(167, 2)
        Me.Cmd_Procesar.Margin = New System.Windows.Forms.Padding(2)
        Me.Cmd_Procesar.Name = "Cmd_Procesar"
        Me.Cmd_Procesar.Size = New System.Drawing.Size(50, 54)
        Me.Cmd_Procesar.TabIndex = 601
        Me.Cmd_Procesar.TabStop = False
        Me.Cmd_Procesar.TextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.Cmd_Procesar.ThemeName = "Aqua"
        '
        'CmdAsignaDelegacion
        '
        Me.CmdAsignaDelegacion.Image = Global.ATMFiscal.My.Resources.Resources.Guardar
        Me.CmdAsignaDelegacion.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdAsignaDelegacion.Location = New System.Drawing.Point(221, 4)
        Me.CmdAsignaDelegacion.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdAsignaDelegacion.Name = "CmdAsignaDelegacion"
        Me.CmdAsignaDelegacion.Size = New System.Drawing.Size(50, 54)
        Me.CmdAsignaDelegacion.TabIndex = 596
        Me.CmdAsignaDelegacion.TabStop = False
        Me.CmdAsignaDelegacion.TextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.CmdAsignaDelegacion.ThemeName = "Aqua"
        '
        'CmdBuscarN
        '
        Me.CmdBuscarN.Image = Global.ATMFiscal.My.Resources.Resources.Importar_Datos
        Me.CmdBuscarN.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdBuscarN.Location = New System.Drawing.Point(113, 2)
        Me.CmdBuscarN.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdBuscarN.Name = "CmdBuscarN"
        Me.CmdBuscarN.Size = New System.Drawing.Size(50, 54)
        Me.CmdBuscarN.TabIndex = 597
        Me.CmdBuscarN.TabStop = False
        Me.CmdBuscarN.TextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.CmdBuscarN.ThemeName = "Aqua"
        '
        'cmdCerrar
        '
        Me.cmdCerrar.Image = Global.ATMFiscal.My.Resources.Resources.Salir
        Me.cmdCerrar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.cmdCerrar.Location = New System.Drawing.Point(5, 2)
        Me.cmdCerrar.Margin = New System.Windows.Forms.Padding(2)
        Me.cmdCerrar.Name = "cmdCerrar"
        Me.cmdCerrar.Size = New System.Drawing.Size(50, 54)
        Me.cmdCerrar.TabIndex = 599
        Me.cmdCerrar.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.cmdCerrar.ThemeName = "Aqua"
        '
        'CmdLimpiarN
        '
        Me.CmdLimpiarN.Image = Global.ATMFiscal.My.Resources.Resources.LIMPIAR
        Me.CmdLimpiarN.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdLimpiarN.Location = New System.Drawing.Point(59, 2)
        Me.CmdLimpiarN.Margin = New System.Windows.Forms.Padding(2)
        Me.CmdLimpiarN.Name = "CmdLimpiarN"
        Me.CmdLimpiarN.Size = New System.Drawing.Size(50, 54)
        Me.CmdLimpiarN.TabIndex = 598
        Me.CmdLimpiarN.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdLimpiarN.ThemeName = "Aqua"
        '
        'RadPanel2
        '
        Me.RadPanel2.Controls.Add(Me.Tabla)
        Me.RadPanel2.Location = New System.Drawing.Point(757, 28)
        Me.RadPanel2.Name = "RadPanel2"
        Me.RadPanel2.Size = New System.Drawing.Size(234, 154)
        Me.RadPanel2.TabIndex = 1
        '
        'Tabla
        '
        Me.Tabla.AllowUserToAddRows = False
        Me.Tabla.AllowUserToDeleteRows = False
        Me.Tabla.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.Tabla.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells
        Me.Tabla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Tabla.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Val, Me.Des})
        Me.Tabla.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Tabla.Location = New System.Drawing.Point(0, 0)
        Me.Tabla.Name = "Tabla"
        Me.Tabla.ReadOnly = True
        Me.Tabla.Size = New System.Drawing.Size(234, 154)
        Me.Tabla.TabIndex = 710
        '
        'Val
        '
        Me.Val.HeaderText = "Valor"
        Me.Val.Name = "Val"
        Me.Val.ReadOnly = True
        Me.Val.Width = 69
        '
        'Des
        '
        Me.Des.HeaderText = "Descripcion"
        Me.Des.Name = "Des"
        Me.Des.ReadOnly = True
        Me.Des.Width = 108
        '
        'TablaImportar
        '
        Me.TablaImportar.AllowDrop = True
        Me.TablaImportar.AllowUserToAddRows = False
        Me.TablaImportar.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCellsExceptHeaders
        Me.TablaImportar.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.TablaImportar.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TablaImportar.Location = New System.Drawing.Point(0, 224)
        Me.TablaImportar.Margin = New System.Windows.Forms.Padding(2)
        Me.TablaImportar.Name = "TablaImportar"
        Me.TablaImportar.RowTemplate.Height = 24
        Me.TablaImportar.Size = New System.Drawing.Size(1041, 295)
        Me.TablaImportar.TabIndex = 3
        '
        'Carga_Masiva_Personal
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.ClientSize = New System.Drawing.Size(1041, 519)
        Me.Controls.Add(Me.TablaImportar)
        Me.Controls.Add(Me.RadPanel2)
        Me.Controls.Add(Me.RadPanel1)
        Me.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Carga_Masiva_Personal"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Carga Masiva Personal"
        Me.ThemeName = "MaterialBlueGrey"
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        Me.RadPanel1.PerformLayout()
        CType(Me.CmdPlantlla, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Cmd_Procesar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdAsignaDelegacion, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdBuscarN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmdCerrar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdLimpiarN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadPanel2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel2.ResumeLayout(False)
        CType(Me.Tabla, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TablaImportar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Ayuda As ToolTip
	Friend WithEvents TemaGeneral As Telerik.WinControls.Themes.MaterialTheme
	Friend WithEvents TemaBotones As Telerik.WinControls.Themes.AquaTheme
	Friend WithEvents RadPanel1 As Telerik.WinControls.UI.RadPanel
	Friend WithEvents CmdPlantlla As Telerik.WinControls.UI.RadButton
	Friend WithEvents Cmd_Procesar As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdAsignaDelegacion As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdBuscarN As Telerik.WinControls.UI.RadButton
	Friend WithEvents cmdCerrar As Telerik.WinControls.UI.RadButton
	Friend WithEvents CmdLimpiarN As Telerik.WinControls.UI.RadButton
	Friend WithEvents lblRegistros As Label
	Friend WithEvents LstTexto As Listas
	Friend WithEvents Label4 As Label
	Friend WithEvents LstGuia As Listas
	Friend WithEvents Label3 As Label
	Friend WithEvents LstSubDelegacion As Listas
	Friend WithEvents Label2 As Label
	Friend WithEvents LstDelegacion As Listas
	Friend WithEvents Label1 As Label
	Friend WithEvents lstCliente As Listas
	Friend WithEvents Label6 As Label
	Friend WithEvents RadPanel2 As Telerik.WinControls.UI.RadPanel
	Friend WithEvents Tabla As DataGridView
	Friend WithEvents Val As DataGridViewTextBoxColumn
	Friend WithEvents Des As DataGridViewTextBoxColumn
	Friend WithEvents TablaImportar As DataGridView
End Class

