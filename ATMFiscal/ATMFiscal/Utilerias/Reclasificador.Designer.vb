<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Reclasificador
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Reclasificador))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Dtfin = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.DtInicio = New Telerik.WinControls.UI.RadDateTimePicker()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Cmd_Procesar = New Telerik.WinControls.UI.RadButton()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lstCliente = New ATMFiscal.Listas()
        Me.Lstctafinal = New ATMFiscal.Listas()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Lstctainicial = New ATMFiscal.Listas()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cmdCerrar = New Telerik.WinControls.UI.RadButton()
        Me.Panel1.SuspendLayout()
        CType(Me.Dtfin, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DtInicio, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Cmd_Procesar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmdCerrar, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.CadetBlue
        Me.Panel1.Controls.Add(Me.cmdCerrar)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.lstCliente)
        Me.Panel1.Controls.Add(Me.Cmd_Procesar)
        Me.Panel1.Controls.Add(Me.Dtfin)
        Me.Panel1.Controls.Add(Me.DtInicio)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(640, 164)
        Me.Panel1.TabIndex = 0
        '
        'Dtfin
        '
        Me.Dtfin.AutoSize = False
        Me.Dtfin.CalendarSize = New System.Drawing.Size(290, 320)
        Me.Dtfin.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.2!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Dtfin.Location = New System.Drawing.Point(323, 99)
        Me.Dtfin.Name = "Dtfin"
        Me.Dtfin.Size = New System.Drawing.Size(299, 41)
        Me.Dtfin.TabIndex = 645
        Me.Dtfin.TabStop = False
        Me.Dtfin.Text = "viernes, 19 de febrero de 2021"
        Me.Dtfin.ThemeName = "MaterialBlueGrey"
        Me.Dtfin.Value = New Date(2021, 2, 19, 12, 2, 23, 431)
        '
        'DtInicio
        '
        Me.DtInicio.AutoSize = False
        Me.DtInicio.CalendarSize = New System.Drawing.Size(290, 320)
        Me.DtInicio.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.2!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DtInicio.Location = New System.Drawing.Point(12, 99)
        Me.DtInicio.Name = "DtInicio"
        Me.DtInicio.Size = New System.Drawing.Size(299, 41)
        Me.DtInicio.TabIndex = 644
        Me.DtInicio.TabStop = False
        Me.DtInicio.Text = "viernes, 19 de febrero de 2021"
        Me.DtInicio.ThemeName = "MaterialBlueGrey"
        Me.DtInicio.Value = New Date(2021, 2, 19, 12, 2, 23, 431)
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(426, 78)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(93, 18)
        Me.Label4.TabIndex = 643
        Me.Label4.Text = "Fecha Final:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(110, 78)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(102, 18)
        Me.Label3.TabIndex = 642
        Me.Label3.Text = "Fecha Inicial:"
        '
        'Cmd_Procesar
        '
        Me.Cmd_Procesar.Image = Global.ATMFiscal.My.Resources.Resources.Procesar
        Me.Cmd_Procesar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.Cmd_Procesar.Location = New System.Drawing.Point(562, 12)
        Me.Cmd_Procesar.Margin = New System.Windows.Forms.Padding(2)
        Me.Cmd_Procesar.Name = "Cmd_Procesar"
        Me.Cmd_Procesar.Size = New System.Drawing.Size(60, 55)
        Me.Cmd_Procesar.TabIndex = 646
        Me.Cmd_Procesar.TabStop = False
        Me.Cmd_Procesar.TextAlignment = System.Drawing.ContentAlignment.TopRight
        Me.Cmd_Procesar.ThemeName = "Aqua"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(8, 5)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 18)
        Me.Label2.TabIndex = 647
        Me.Label2.Text = "Empresa:"
        '
        'lstCliente
        '
        Me.lstCliente.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lstCliente.Location = New System.Drawing.Point(12, 29)
        Me.lstCliente.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.lstCliente.Name = "lstCliente"
        Me.lstCliente.SelectItem = ""
        Me.lstCliente.SelectText = ""
        Me.lstCliente.Size = New System.Drawing.Size(423, 36)
        Me.lstCliente.TabIndex = 648
        '
        'Lstctafinal
        '
        Me.Lstctafinal.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lstctafinal.Location = New System.Drawing.Point(12, 273)
        Me.Lstctafinal.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Lstctafinal.Name = "Lstctafinal"
        Me.Lstctafinal.SelectItem = ""
        Me.Lstctafinal.SelectText = ""
        Me.Lstctafinal.Size = New System.Drawing.Size(610, 36)
        Me.Lstctafinal.TabIndex = 561
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(12, 251)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(117, 18)
        Me.Label7.TabIndex = 560
        Me.Label7.Text = "Cuenta Destino:"
        '
        'Lstctainicial
        '
        Me.Lstctainicial.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lstctainicial.Location = New System.Drawing.Point(12, 198)
        Me.Lstctainicial.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
        Me.Lstctainicial.Name = "Lstctainicial"
        Me.Lstctainicial.SelectItem = ""
        Me.Lstctainicial.SelectText = ""
        Me.Lstctainicial.Size = New System.Drawing.Size(610, 36)
        Me.Lstctainicial.TabIndex = 559
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(10, 175)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(111, 18)
        Me.Label5.TabIndex = 558
        Me.Label5.Text = "Cuenta Origen:"
        '
        'cmdCerrar
        '
        Me.cmdCerrar.Image = Global.ATMFiscal.My.Resources.Resources.Salir
        Me.cmdCerrar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.cmdCerrar.Location = New System.Drawing.Point(495, 11)
        Me.cmdCerrar.Margin = New System.Windows.Forms.Padding(2)
        Me.cmdCerrar.Name = "cmdCerrar"
        Me.cmdCerrar.Size = New System.Drawing.Size(63, 55)
        Me.cmdCerrar.TabIndex = 649
        Me.cmdCerrar.TextAlignment = System.Drawing.ContentAlignment.BottomRight
        Me.cmdCerrar.ThemeName = "Aqua"
        '
        'Reclasificador
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.SteelBlue
        Me.ClientSize = New System.Drawing.Size(640, 324)
        Me.ControlBox = False
        Me.Controls.Add(Me.Lstctafinal)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Lstctainicial)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Reclasificador"
        '
        '
        '
        Me.RootElement.ApplyShapeToControl = True
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Reclasificador"
        Me.ThemeName = "MaterialBlueGrey"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.Dtfin, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DtInicio, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Cmd_Procesar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmdCerrar, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Panel1 As Panel
    Friend WithEvents Dtfin As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents DtInicio As Telerik.WinControls.UI.RadDateTimePicker
    Friend WithEvents Label4 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Cmd_Procesar As Telerik.WinControls.UI.RadButton
    Friend WithEvents Label2 As Label
    Friend WithEvents lstCliente As Listas
    Friend WithEvents Lstctafinal As Listas
    Friend WithEvents Label7 As Label
    Friend WithEvents Lstctainicial As Listas
    Friend WithEvents Label5 As Label
    Friend WithEvents cmdCerrar As Telerik.WinControls.UI.RadButton
End Class

