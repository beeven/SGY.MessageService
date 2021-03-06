﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GZCustoms.Application.SGY.MessageService.UnitTest
{
    public class Constants
    {
        public static String ClientMessage = "<DeclEnvelop xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:noNamespaceSchemaLocation=\"DECL_FILE.xsd\">"
                + "  <EnvelopHead>"
                + "    <Name>元亨报关</Name>"
                + "    <Version>1.0</Version>"
                + "    <From>111</From>"
                + "    <To>广州海关</To>"
                + "    <Operation>2</Operation>"
                + "    <SendTime>2014-12-29 14:54:03</SendTime>"
                + "    <Guid>b7e778c905a84c768da3adfad31746d0</Guid>"
                + "  </EnvelopHead>"
                + "  <EnvelopBody>"
                + "    <DECL_HEAD>"
                + "      <OID>20141225140804717</OID>"
                + "      <SEQ_NO xsi:nil=\"true\" />"
                + "      <ENTRY_ID xsi:nil=\"true\" />"
                + "      <PRE_ENTRY_ID>994953404</PRE_ENTRY_ID>"
                + "      <DECL_PORT>5207</DECL_PORT>"
                + "      <I_E_PORT>5165</I_E_PORT>"
                + "      <ENTRY_TYPE>M</ENTRY_TYPE>"
                + "      <MANUAL_NO xsi:nil=\"true\" />"
                + "      <CONTR_NO>CG14060032</CONTR_NO>"
                + "      <I_E_FLAG>I</I_E_FLAG>"
                + "      <I_E_DATE>2014-12-25</I_E_DATE>"
                + "      <D_DATE>2014-12-29</D_DATE>"
                + "      <MOD_NUM xsi:nil=\"true\" />"
                + "      <AGENT_CODE>4422980013</AGENT_CODE>"
                + "      <AGENT_NAME>佛山市美的报关有限公司</AGENT_NAME>"
                + "      <OWNER_CODE>4422931356</OWNER_CODE>"
                + "      <OWNER_NAME>广东美的厨房电器有限公司</OWNER_NAME>"
                + "      <TRADE_CO>4422931356</TRADE_CO>"
                + "      <TRADE_NAME>广东美的厨房电器有限公司</TRADE_NAME>"
                + "      <CO_OWNER xsi:nil=\"true\" />"
                + "      <TRAF_MODE>1</TRAF_MODE>"
                + "      <TRAF_NAME>5101500226</TRAF_NAME>"
                + "      <BILL_NO>BJ141306</BILL_NO>"
                + "      <VOYAGE_NO>515401408050</VOYAGE_NO>"
                + "      <TRADE_MODE>0110</TRADE_MODE>"
                + "      <CUT_MODE>101</CUT_MODE>"
                + "      <PAY_MODE xsi:nil=\"true\" />"
                + "      <SERVICE_FEE xsi:nil=\"true\" />"
                + "      <TRADE_COUNTRY>110</TRADE_COUNTRY>"
                + "      <PAYMENT_MARK xsi:nil=\"true\" />"
                + "      <LICENSE_NO xsi:nil=\"true\" />"
                + "      <DESTINATION_PORT>110</DESTINATION_PORT>"
                + "      <DESTINATION_CODE>51069</DESTINATION_CODE>"
                + "      <TRANS_MODE>1</TRANS_MODE>"
                + "      <PACK_NO>14</PACK_NO>"
                + "      <GROSS_WT>37.12500</GROSS_WT>"
                + "      <NET_WT>26.73000</NET_WT>"
                + "      <WRAP_TYPE>2</WRAP_TYPE>"
                + "      <FEE_CURR xsi:nil=\"true\" />"
                + "      <FEE_MARK xsi:nil=\"true\" />"
                + "      <FEE_RATE xsi:nil=\"true\" />"
                + "      <INSUR_CURR xsi:nil=\"true\" />"
                + "      <INSUR_MARK xsi:nil=\"true\" />"
                + "      <INSUR_RATE xsi:nil=\"true\" />"
                + "      <OTHER_CURR xsi:nil=\"true\" />"
                + "      <OTHER_MARK xsi:nil=\"true\" />"
                + "      <OTHER_RATE xsi:nil=\"true\" />"
                + "      <IN_RATIO xsi:nil=\"true\" />"
                + "      <APPR_NO xsi:nil=\"true\" />"
                + "      <RELATIVE_ID xsi:nil=\"true\" />"
                + "      <RELATIVE_MANUAL_NO xsi:nil=\"true\" />"
                + "      <BONDED_NO xsi:nil=\"true\" />"
                + "      <CUSTOMS_FIELD>5154</CUSTOMS_FIELD>"
                + "      <EDI_ID xsi:nil=\"true\" />"
                + "      <EDI_REMARK xsi:nil=\"true\" />"
                + "      <PARTENER_ID xsi:nil=\"true\" />"
                + "      <P_DATE xsi:nil=\"true\" />"
                + "      <TYPIST_NO>9200000005321</TYPIST_NO>"
                + "      <TYPIST_NAME>凌志铭</TYPIST_NAME>"
                + "      <DECLARE_NO xsi:nil=\"true\" />"
                + "      <BP_NO xsi:nil=\"true\" />"
                + "      <NOTE_S xsi:nil=\"true\" />"
                + "      <STATE>1</STATE>"
                + "      <IS_CREATE xsi:nil=\"true\" />"
                + "      <COP_CODE>190784351</COP_CODE>"
                + "      <COP_NAME>广州市海通科技服务有限公司</COP_NAME>"
                + "      <ASS_CUSTOMS_CODE>T1907843510020141225f4ff60bd7</ASS_CUSTOMS_CODE>"
                + "      <LIST_NO>CUSQ000000125</LIST_NO>"
                + "      <Container_Info xsi:nil=\"true\" />"
                + "      <UploadDate xsi:nil=\"true\" />"
                + "      <Att_Code xsi:nil=\"true\" />"
                + "      <CUSTOMS_CIQ_NO>1141225510000048</CUSTOMS_CIQ_NO>"
                + "      <Update_Date>2014-8-7 14:30:00</Update_Date>"
                + "      <ENTRY_TRANSIT_TYPE>003</ENTRY_TRANSIT_TYPE>"
                + "    </DECL_HEAD>"
                + "    <DECL_LISTS>"
                + "      <DECL_LIST>"
                + "        <OID>201412251408047171</OID>"
                + "        <SEQ_NO xsi:nil=\"true\" />"
                + "        <G_NO>000000001</G_NO>"
                + "        <PRE_ENTRY_ID xsi:nil=\"true\" />"
                + "        <CODE_TS>8537109090</CODE_TS>"
                + "        <CLASS_MARK xsi:nil=\"true\" />"
                + "        <G_NAME>控制板组件</G_NAME>"
                + "        <G_MODEL xsi:nil=\"true\" />"
                + "        <ORIGIN_COUNTRY>142</ORIGIN_COUNTRY>"
                + "        <CONTR_ITEM xsi:nil=\"true\" />"
                + "        <G_QTY>2970.0000</G_QTY>"
                + "        <G_UNIT>007</G_UNIT>"
                + "        <QTY_1>2970.0000</QTY_1>"
                + "        <UNIT_1>007</UNIT_1>"
                + "        <QTY_2 xsi:nil=\"true\" />"
                + "        <UNIT_2>035</UNIT_2>"
                + "        <TRADE_CURR>502</TRADE_CURR>"
                + "        <DECL_PRICE>1.5676</DECL_PRICE>"
                + "        <DECL_TOTAL>4655.77</DECL_TOTAL>"
                + "        <USE_TO>11</USE_TO>"
                + "        <DUTY_MODE>1</DUTY_MODE>"
                + "        <WORK_USD xsi:nil=\"true\" />"
                + "        <PRDT_NO xsi:nil=\"true\" />"
                + "        <GOODS_ID xsi:nil=\"true\" />"
                + "        <NOTE_S xsi:nil=\"true\" />"
                + "        <LIST_NO>CUSQ000000125</LIST_NO>"
                + "        <CUSTOMS_CIQ_NO xsi:nil=\"true\" />"
                + "      </DECL_LIST>"
                + "    </DECL_LISTS>"
                + "    <DECL_CONTAINERS />"
                + "    <DECL_CERTIFICATES />"
                + "  </EnvelopBody>"
                + "</DeclEnvelop>";


        public static String ClientKey = "141224926731";
        public static String ClientMachineCode = "ABCDEFGHIJKL";
    }
}
