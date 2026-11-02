Public Class Calculaor

    ' Number buttons: 1 to 9
    Private Sub number_Click(sender As Object, e As EventArgs) Handles one.Click, two.Click, three.Click, four.Click, five.Click, six.Click, seven.Click, eight.Click, nine.Click

        Dim btn As Button = TryCast(sender, Button)

        If btn IsNot Nothing Then
            TextBox1.Text &= btn.Text
        End If

    End Sub

    ' Zero button
    Private Sub zero_Click(sender As Object, e As EventArgs) Handles zero.Click

        TextBox1.Text &= "0"

    End Sub

    ' Decimal button
    Private Sub dot_Click(sender As Object, e As EventArgs) Handles dot.Click

        Dim parts() As String = TextBox1.Text.Split("+"c, "-"c, "*"c, "/"c, "%"c)

        If parts(parts.Length - 1).Contains(".") Then
            Exit Sub
        End If

        If TextBox1.Text = "" OrElse
           TextBox1.Text.EndsWith("+") OrElse
           TextBox1.Text.EndsWith("-") OrElse
           TextBox1.Text.EndsWith("*") OrElse
           TextBox1.Text.EndsWith("/") Then
            TextBox1.Text &= "0."
        Else
            TextBox1.Text &= "."
        End If

    End Sub

    ' Cancel button: clear everything
    Private Sub cancel_Click(sender As Object, e As EventArgs) Handles cancel.Click

        TextBox1.Clear()

    End Sub

    ' Clear button: delete one character
    Private Sub clear_Click(sender As Object, e As EventArgs) Handles clear.Click

        If TextBox1.Text.Length > 0 Then
            TextBox1.Text = TextBox1.Text.Substring(0, TextBox1.Text.Length - 1)
        End If

    End Sub

    ' Operator buttons
    Private Sub add_Click(sender As Object, e As EventArgs) Handles add.Click, subtraction.Click, multiplication.Click, divide.Click, percentage.Click

        Dim btn As Button = TryCast(sender, Button)

        If btn Is Nothing OrElse TextBox1.Text = "" Then
            Exit Sub
        End If

        Dim lastChar As Char = TextBox1.Text(TextBox1.Text.Length - 1)

        If "+-*/%".Contains(lastChar) Then

            TextBox1.Text = TextBox1.Text.Substring(0, TextBox1.Text.Length - 1)

        End If

        If btn.Name = "percentage" Then
            TextBox1.Text &= "%"
        Else
            TextBox1.Text &= btn.Text
        End If

    End Sub

    ' Equal button: calculate result
    Private Sub equal_Click(sender As Object, e As EventArgs) Handles equal.Click

        Try

            Dim expression As String = TextBox1.Text.Trim()

            If expression = "" Then Exit Sub

            If "+-*/%".Contains(expression(expression.Length - 1)) Then
                TextBox1.Text = "Wrong Entry"
                Exit Sub
            End If

            ' Convert percentage into division by 100
            expression = expression.Replace("%", "/100.0")

            Dim table As New System.Data.DataTable()

            Dim result As Object = table.Compute(expression, String.Empty)

            TextBox1.Text = Convert.ToString(
                result,
                Globalization.CultureInfo.InvariantCulture
            )

        Catch ex As Exception

            TextBox1.Text = "Wrong Entry"

        End Try

    End Sub

    ' Enable Enter key
    Private Sub form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.KeyPreview = True

    End Sub

    ' Enter key: show result
    Private Sub Calculaor_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown

        If e.KeyCode = Keys.Enter Then

            equal.PerformClick()
            e.SuppressKeyPress = True

        End If

    End Sub

End Class