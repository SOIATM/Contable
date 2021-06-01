<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Tabla_Filtro
    Inherits System.Windows.Forms.UserControl

    'UserControl reemplaza a Dispose para limpiar la lista de componentes.
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

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim RadListDataItem1 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem2 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem3 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem4 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem5 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem6 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem7 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem8 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem9 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem10 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem11 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim RadListDataItem12 As Telerik.WinControls.UI.RadListDataItem = New Telerik.WinControls.UI.RadListDataItem()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Tabla_Filtro))
        Dim TableViewDefinition1 As Telerik.WinControls.UI.TableViewDefinition = New Telerik.WinControls.UI.TableViewDefinition()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.TemaGeneral = New Telerik.WinControls.Themes.MaterialTheme()
        Me.RadPanel1 = New Telerik.WinControls.UI.RadPanel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblSqlInsert = New System.Windows.Forms.Label()
        Me.LblSqlSelect = New System.Windows.Forms.Label()
        Me.txtfiltro = New System.Windows.Forms.TextBox()
        Me.RadGroupBox2 = New Telerik.WinControls.UI.RadGroupBox()
        Me.lstfiltro = New ATMFiscal.Listas()
        Me.lblCAMPO = New System.Windows.Forms.Label()
        Me.RadGroupBox1 = New Telerik.WinControls.UI.RadGroupBox()
        Me.ComboMes = New Telerik.WinControls.UI.RadDropDownList()
        Me.ComboAño = New Telerik.WinControls.UI.RadDropDownList()
        Me.CmdActualizar = New Telerik.WinControls.UI.RadButton()
        Me.CmdExportaExcel = New Telerik.WinControls.UI.RadButton()
        Me.CmdEliminar = New Telerik.WinControls.UI.RadButton()
        Me.CmdGuardar = New Telerik.WinControls.UI.RadButton()
        Me.CmdNuevo = New Telerik.WinControls.UI.RadButton()
        Me.CmdCerrar = New Telerik.WinControls.UI.RadButton()
        Me.RadGridView2 = New Telerik.WinControls.UI.RadGridView()
        Me.TemaBotones = New Telerik.WinControls.Themes.AquaTheme()
        Me.Tabla = New ATMFiscal.Tablas()
        Me.Ayuda = New System.Windows.Forms.ToolTip(Me.components)
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadPanel1.SuspendLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox2.SuspendLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.RadGroupBox1.SuspendLayout()
        CType(Me.ComboMes, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ComboAño, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdActualizar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdExportaExcel, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdEliminar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdGuardar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdNuevo, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdCerrar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.RadGridView2.MasterTemplate, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Tabla, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'RadPanel1
        '
        Me.RadPanel1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.RadPanel1.Controls.Add(Me.Label4)
        Me.RadPanel1.Controls.Add(Me.Label5)
        Me.RadPanel1.Controls.Add(Me.lblSqlInsert)
        Me.RadPanel1.Controls.Add(Me.LblSqlSelect)
        Me.RadPanel1.Controls.Add(Me.txtfiltro)
        Me.RadPanel1.Controls.Add(Me.RadGroupBox2)
        Me.RadPanel1.Controls.Add(Me.RadGroupBox1)
        Me.RadPanel1.Controls.Add(Me.CmdActualizar)
        Me.RadPanel1.Controls.Add(Me.CmdExportaExcel)
        Me.RadPanel1.Controls.Add(Me.CmdEliminar)
        Me.RadPanel1.Controls.Add(Me.CmdGuardar)
        Me.RadPanel1.Controls.Add(Me.CmdNuevo)
        Me.RadPanel1.Controls.Add(Me.CmdCerrar)
        Me.RadPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.RadPanel1.Location = New System.Drawing.Point(0, 0)
        Me.RadPanel1.Name = "RadPanel1"
        Me.RadPanel1.Size = New System.Drawing.Size(1495, 95)
        Me.RadPanel1.TabIndex = 0
        Me.RadPanel1.ThemeName = "Material"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(1202, 32)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(136, 21)
        Me.Label4.TabIndex = 20
        Me.Label4.Text = "Total de registros"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(1354, 32)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(0, 21)
        Me.Label5.TabIndex = 19
        '
        'lblSqlInsert
        '
        Me.lblSqlInsert.AutoSize = True
        Me.lblSqlInsert.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSqlInsert.Location = New System.Drawing.Point(1391, 28)
        Me.lblSqlInsert.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSqlInsert.Name = "lblSqlInsert"
        Me.lblSqlInsert.Size = New System.Drawing.Size(43, 17)
        Me.lblSqlInsert.TabIndex = 27
        Me.lblSqlInsert.Text = "insert"
        Me.lblSqlInsert.Visible = False
        '
        'LblSqlSelect
        '
        Me.LblSqlSelect.AutoSize = True
        Me.LblSqlSelect.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblSqlSelect.Location = New System.Drawing.Point(1446, 28)
        Me.LblSqlSelect.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LblSqlSelect.Name = "LblSqlSelect"
        Me.LblSqlSelect.Size = New System.Drawing.Size(45, 17)
        Me.LblSqlSelect.TabIndex = 26
        Me.LblSqlSelect.Text = "select"
        Me.LblSqlSelect.Visible = False
        '
        'txtfiltro
        '
        Me.txtfiltro.Location = New System.Drawing.Point(1394, 49)
        Me.txtfiltro.Margin = New System.Windows.Forms.Padding(4)
        Me.txtfiltro.Name = "txtfiltro"
        Me.txtfiltro.Size = New System.Drawing.Size(115, 22)
        Me.txtfiltro.TabIndex = 25
        Me.txtfiltro.Visible = False
        '
        'RadGroupBox2
        '
        Me.RadGroupBox2.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox2.BackColor = System.Drawing.Color.LightSteelBlue
        Me.RadGroupBox2.Controls.Add(Me.lstfiltro)
        Me.RadGroupBox2.Controls.Add(Me.lblCAMPO)
        Me.RadGroupBox2.HeaderText = "Filtro"
        Me.RadGroupBox2.Location = New System.Drawing.Point(742, 10)
        Me.RadGroupBox2.Name = "RadGroupBox2"
        Me.RadGroupBox2.Size = New System.Drawing.Size(453, 70)
        Me.RadGroupBox2.TabIndex = 24
        Me.RadGroupBox2.Text = "Filtro"
        Me.RadGroupBox2.ThemeName = "Material"
        '
        'lstfiltro
        '
        Me.lstfiltro.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstfiltro.Location = New System.Drawing.Point(171, 14)
        Me.lstfiltro.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lstfiltro.Name = "lstfiltro"
        Me.lstfiltro.SelectItem = ""
        Me.lstfiltro.SelectText = ""
        Me.lstfiltro.Size = New System.Drawing.Size(269, 50)
        Me.lstfiltro.TabIndex = 23
        '
        'lblCAMPO
        '
        Me.lblCAMPO.AutoSize = True
        Me.lblCAMPO.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCAMPO.Location = New System.Drawing.Point(13, 29)
        Me.lblCAMPO.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCAMPO.Name = "lblCAMPO"
        Me.lblCAMPO.Size = New System.Drawing.Size(0, 21)
        Me.lblCAMPO.TabIndex = 21
        '
        'RadGroupBox1
        '
        Me.RadGroupBox1.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping
        Me.RadGroupBox1.BackColor = System.Drawing.Color.LightSteelBlue
        Me.RadGroupBox1.Controls.Add(Me.ComboMes)
        Me.RadGroupBox1.Controls.Add(Me.ComboAño)
        Me.RadGroupBox1.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RadGroupBox1.HeaderText = "             Año                 /              Mes"
        Me.RadGroupBox1.Location = New System.Drawing.Point(371, 10)
        Me.RadGroupBox1.Name = "RadGroupBox1"
        Me.RadGroupBox1.Size = New System.Drawing.Size(292, 70)
        Me.RadGroupBox1.TabIndex = 22
        Me.RadGroupBox1.Text = "             Año                 /              Mes"
        Me.RadGroupBox1.ThemeName = "Material"
        '
        'ComboMes
        '
        Me.ComboMes.Font = New System.Drawing.Font("Franklin Gothic Medium", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        RadListDataItem1.Text = "01"
        RadListDataItem2.Text = "02"
        RadListDataItem3.Text = "03"
        RadListDataItem4.Text = "04"
        RadListDataItem5.Text = "05"
        RadListDataItem6.Text = "06"
        RadListDataItem7.Text = "07"
        RadListDataItem8.Text = "08"
        RadListDataItem9.Text = "09"
        RadListDataItem10.Text = "10"
        RadListDataItem11.Text = "11"
        RadListDataItem12.Text = "12"
        Me.ComboMes.Items.Add(RadListDataItem1)
        Me.ComboMes.Items.Add(RadListDataItem2)
        Me.ComboMes.Items.Add(RadListDataItem3)
        Me.ComboMes.Items.Add(RadListDataItem4)
        Me.ComboMes.Items.Add(RadListDataItem5)
        Me.ComboMes.Items.Add(RadListDataItem6)
        Me.ComboMes.Items.Add(RadListDataItem7)
        Me.ComboMes.Items.Add(RadListDataItem8)
        Me.ComboMes.Items.Add(RadListDataItem9)
        Me.ComboMes.Items.Add(RadListDataItem10)
        Me.ComboMes.Items.Add(RadListDataItem11)
        Me.ComboMes.Items.Add(RadListDataItem12)
        Me.ComboMes.Location = New System.Drawing.Point(157, 22)
        Me.ComboMes.Name = "ComboMes"
        Me.ComboMes.Size = New System.Drawing.Size(130, 36)
        Me.ComboMes.TabIndex = 24
        Me.ComboMes.ThemeName = "Material"
        '
        'ComboAño
        '
        Me.ComboAño.Font = New System.Drawing.Font("Franklin Gothic Medium", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ComboAño.Location = New System.Drawing.Point(14, 22)
        Me.ComboAño.Name = "ComboAño"
        Me.ComboAño.Size = New System.Drawing.Size(130, 36)
        Me.ComboAño.TabIndex = 23
        Me.ComboAño.ThemeName = "Material"
        '
        'CmdActualizar
        '
        Me.CmdActualizar.Image = Global.ATMFiscal.My.Resources.Resources.Actualizar
        Me.CmdActualizar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdActualizar.Location = New System.Drawing.Point(669, 11)
        Me.CmdActualizar.Name = "CmdActualizar"
        Me.CmdActualizar.Size = New System.Drawing.Size(67, 67)
        Me.CmdActualizar.TabIndex = 3
        Me.CmdActualizar.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdActualizar.ThemeName = "Aqua"
        '
        'CmdExportaExcel
        '
        Me.CmdExportaExcel.Image = Global.ATMFiscal.My.Resources.Resources.Exportar
        Me.CmdExportaExcel.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdExportaExcel.Location = New System.Drawing.Point(298, 11)
        Me.CmdExportaExcel.Name = "CmdExportaExcel"
        Me.CmdExportaExcel.Size = New System.Drawing.Size(67, 67)
        Me.CmdExportaExcel.TabIndex = 2
        Me.CmdExportaExcel.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdExportaExcel.ThemeName = "Aqua"
        '
        'CmdEliminar
        '
        Me.CmdEliminar.Image = Global.ATMFiscal.My.Resources.Resources.Eliminar
        Me.CmdEliminar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdEliminar.Location = New System.Drawing.Point(225, 11)
        Me.CmdEliminar.Name = "CmdEliminar"
        Me.CmdEliminar.Size = New System.Drawing.Size(67, 67)
        Me.CmdEliminar.TabIndex = 2
        Me.CmdEliminar.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdEliminar.ThemeName = "Aqua"
        '
        'CmdGuardar
        '
        Me.CmdGuardar.Image = Global.ATMFiscal.My.Resources.Resources.Guardar
        Me.CmdGuardar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdGuardar.Location = New System.Drawing.Point(152, 11)
        Me.CmdGuardar.Name = "CmdGuardar"
        Me.CmdGuardar.Size = New System.Drawing.Size(67, 67)
        Me.CmdGuardar.TabIndex = 2
        Me.CmdGuardar.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdGuardar.ThemeName = "Aqua"
        '
        'CmdNuevo
        '
        Me.CmdNuevo.Image = Global.ATMFiscal.My.Resources.Resources.Nuevo
        Me.CmdNuevo.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdNuevo.Location = New System.Drawing.Point(79, 11)
        Me.CmdNuevo.Name = "CmdNuevo"
        Me.CmdNuevo.Size = New System.Drawing.Size(67, 67)
        Me.CmdNuevo.TabIndex = 1
        Me.CmdNuevo.TabStop = False
        Me.CmdNuevo.TextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.CmdNuevo.ThemeName = "Aqua"
        '
        'CmdCerrar
        '
        Me.CmdCerrar.Image = CType(resources.GetObject("CmdCerrar.Image"), System.Drawing.Image)
        Me.CmdCerrar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdCerrar.Location = New System.Drawing.Point(6, 11)
        Me.CmdCerrar.Name = "CmdCerrar"
        Me.CmdCerrar.Size = New System.Drawing.Size(67, 67)
        Me.CmdCerrar.TabIndex = 0
        Me.CmdCerrar.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.CmdCerrar.ThemeName = "Aqua"
        '
        'RadGridView2
        '
        Me.RadGridView2.Location = New System.Drawing.Point(113, 60)
        '
        '
        '
        Me.RadGridView2.MasterTemplate.ViewDefinition = TableViewDefinition1
        Me.RadGridView2.Name = "RadGridView2"
        Me.RadGridView2.Size = New System.Drawing.Size(300, 187)
        Me.RadGridView2.TabIndex = 0
        '
        'Tabla
        '
        Me.Tabla.AllowUserToAddRows = False
        Me.Tabla.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.Tabla.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Tabla.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical
        Me.Tabla.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.CadetBlue
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold)
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Tabla.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.Tabla.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.Tabla.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Tabla.EnableHeadersVisualStyles = False
        Me.Tabla.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Regular)
        Me.Tabla.GridColor = System.Drawing.Color.CadetBlue
        Me.Tabla.Location = New System.Drawing.Point(0, 95)
        Me.Tabla.Name = "Tabla"
        Me.Tabla.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Regular)
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Crimson
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Tabla.RowHeadersDefaultCellStyle = DataGridViewCellStyle2
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.WhiteSmoke
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Crimson
        Me.Tabla.RowsDefaultCellStyle = DataGridViewCellStyle3
        Me.Tabla.RowTemplate.Height = 24
        Me.Tabla.Size = New System.Drawing.Size(1495, 289)
        Me.Tabla.TabIndex = 1
        '
        'Tabla_Filtro
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.LightSteelBlue
        Me.Controls.Add(Me.Tabla)
        Me.Controls.Add(Me.RadPanel1)
        Me.Name = "Tabla_Filtro"
        Me.Size = New System.Drawing.Size(1495, 384)
        CType(Me.RadPanel1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadPanel1.ResumeLayout(False)
        Me.RadPanel1.PerformLayout()
        CType(Me.RadGroupBox2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox2.ResumeLayout(False)
        Me.RadGroupBox2.PerformLayout()
        CType(Me.RadGroupBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.RadGroupBox1.ResumeLayout(False)
        Me.RadGroupBox1.PerformLayout()
        CType(Me.ComboMes, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ComboAño, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdActualizar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdExportaExcel, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdEliminar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdGuardar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdNuevo, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdCerrar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGridView2.MasterTemplate, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.RadGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Tabla, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TemaGeneral As Telerik.WinControls.Themes.MaterialTheme
    Friend WithEvents RadPanel1 As Telerik.WinControls.UI.RadPanel
    Friend WithEvents CmdCerrar As Telerik.WinControls.UI.RadButton
    Friend WithEvents RadGridView2 As Telerik.WinControls.UI.RadGridView
    Friend WithEvents CmdNuevo As Telerik.WinControls.UI.RadButton
    Friend WithEvents TemaBotones As Telerik.WinControls.Themes.AquaTheme
    Friend WithEvents CmdExportaExcel As Telerik.WinControls.UI.RadButton
    Friend WithEvents CmdEliminar As Telerik.WinControls.UI.RadButton
    Friend WithEvents CmdGuardar As Telerik.WinControls.UI.RadButton
    Friend WithEvents CmdActualizar As Telerik.WinControls.UI.RadButton
    Friend WithEvents Tabla As Tablas
    Friend WithEvents Label5 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents RadGroupBox1 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents lblCAMPO As Label
    Friend WithEvents ComboAño As Telerik.WinControls.UI.RadDropDownList
    Friend WithEvents ComboMes As Telerik.WinControls.UI.RadDropDownList
    Friend WithEvents lstfiltro As Listas
    Friend WithEvents RadGroupBox2 As Telerik.WinControls.UI.RadGroupBox
    Friend WithEvents txtfiltro As TextBox
    Friend WithEvents lblSqlInsert As Label
    Friend WithEvents LblSqlSelect As Label
    Friend WithEvents Ayuda As ToolTip
End Class
