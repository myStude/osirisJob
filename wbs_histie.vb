Imports System.Net
Imports System.IO

Public Class wbs_histie
    Public MysqlRepository As New MySql_Repository
    Public LogWriter As New LogWriter

    Private Property wbsIE As String = My.Settings.IEHistoricoContrato
    Private Property ProxyCFG As String = My.Settings.ProxyCFG
    Private Property ProxyUsr As String = My.Settings.ProxyUsr
    Private Property ProxyPss As String = My.Settings.ProxyPss
    Private Property ProxyDom As String = My.Settings.ProxyDom

    ''' <summary>
    ''' FUNÇÃO ATUALIZA TABELAS WEBSERVICE IE VOCALCOM (LIST)
    ''' </summary>
    ''' <remarks>ENJOY</remarks>
    Sub WbsVocalcom()

        Try
            'Cria a requisão HTTP
            Dim req As WebRequest = WebRequest.Create(wbsIE)

            'Configura as informações do Proxy
            Dim proxy As New WebProxy(ProxyCFG, 8080)
            proxy.Credentials = New NetworkCredential(ProxyUsr, ProxyPss, ProxyDom)

            'Informa o Proxy, configurado como o Proxy padrão para o WebRequest
            req.Proxy = proxy

            'Executa a requisão do XML do endereço, utilizando Proxy
            Dim xmlStream As Stream = req.GetResponse().GetResponseStream()

            Dim ds As New DataSet()
            'Usa o Stream obtido pela requisão como fonte do DataSet
            ds.ReadXml(xmlStream)

            Dim str As String

            '// EACH ROWS
            For Each TRows In ds.Tables(0).Rows
                str = "(" & _
                    TRows("ID").ToString() & "," & _
                    TRows("ID_IE").ToString() & ",'" & _
                    TRows("DATA").ToString() & "'," & _
                    TRows("CONTRATO").ToString() & "," & _
                    TRows("OCORRENCIA").ToString() & "," & _
                    TRows("MOTIVO_ID").ToString() & ",'" & _
                    TRows("MOTIVO").ToString() & "'," & _
                    TRows("STATUS_ID").ToString() & ",'" & _
                    TRows("STATUS").ToString() & "','" & _
                    TRows("DETALHE").Replace("/", "-").ToString() & "','" & _
                    TRows("MOMENTO").ToString() & "'," & _
                    TRows("LOGIN_AGENTE").ToString() & "," & _
                    TRows("COD_OPERADORA").ToString() & ",'" & _
                    TRows("CIDADE").ToString() & "')"

                '// MYSQL ADD DATA
                MysqlRepository.MysqlAdd("IGNORE", "ora_wbs.wbs_tabie", str, "")
            Next

            ds.Dispose()
        Catch ex As Exception
            LogWriter.WhriteLog(Now, ex.Message, "WEB HIST IE VOCALCOM")
        End Try

    End Sub

End Class
