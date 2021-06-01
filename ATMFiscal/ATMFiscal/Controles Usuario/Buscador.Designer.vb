<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Buscador
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
        Me.TxtFiltro = New Telerik.WinControls.UI.RadTextBox()
        Me.CmdBuscar = New Telerik.WinControls.UI.RadButton()
        CType(Me.TxtFiltro, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CmdBuscar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TxtFiltro
        '
        Me.TxtFiltro.AutoSize = False
        Me.TxtFiltro.Font = New System.Drawing.Font("Franklin Gothic Medium", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFiltro.Location = New System.Drawing.Point(3, 3)
        Me.TxtFiltro.Name = "TxtFiltro"
        Me.TxtFiltro.Size = New System.Drawing.Size(380, 36)
        Me.TxtFiltro.TabIndex = 4
        Me.TxtFiltro.ThemeName = "Material"
        '
        'CmdBuscar
        '
        Me.CmdBuscar.Font = New System.Drawing.Font("Franklin Gothic Medium", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdBuscar.ForeColor = System.Drawing.Color.MidnightBlue
        Me.CmdBuscar.Image = Global.ATMFiscal.My.Resources.Resources.Buscar
        Me.CmdBuscar.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter
        Me.CmdBuscar.Location = New System.Drawing.Point(389, 2)
        Me.CmdBuscar.Name = "CmdBuscar"
        Me.CmdBuscar.Size = New System.Drawing.Size(107, 39)
        Me.CmdBuscar.TabIndex = 5
        Me.CmdBuscar.TabStop = False
        Me.CmdBuscar.ThemeName = "Aqua"
        '
        'Buscador
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Transparent
        Me.Controls.Add(Me.CmdBuscar)
        Me.Controls.Add(Me.TxtFiltro)
        Me.Name = "Buscador"
        Me.Size = New System.Drawing.Size(505, 43)
        CType(Me.TxtFiltro, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CmdBuscar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents TxtFiltro As Telerik.WinControls.UI.RadTextBox
    Friend WithEvents CmdBuscar As Telerik.WinControls.UI.RadButton
End Class
