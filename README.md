广州海关“三个一”单一窗口接口说明
===========
广州海关技术处 2015/5/15


本接口支持 http 协议与 soap 1.1 协议




[TOC]


http 协议
------------
基础地址
```bash
http://183.63.251.70/SingleWindow/SingleWindowMessageService.svc/json/
```
_ _ _

###获取关键关联号
此方法用于获取一个关捡关联号。
```bash
http://183.63.251.70/SingleWindow/SingleWindowMessageService.svc/json/{ieFlag}/{locationCode}
```
###### Remarks
http协议使用 GET 方式

###### Parameters
*ieFlag*
>进出口标志,可以为以下值之一
>0 出口
>1 进口

*locationCode*
>关区代码，长4位
>如: 5144

###### Return Value
返回一个 *CusCiqReceipt* 对象，包含以下字段

*GetCusCiqResult*
> 关捡关联号，长度21位
> 如：0150515514400001

###### Examples
```JSON
{"GetCusCiqNumResult":"1150515514400001"}
```

_ _ _

### 上传报关单数据
此方法用于上传报关数据。
```bash
http://183.63.251.70/SingleWindow/SingleWindowMessageService.svc/json/
```
###### Remarks
http协议使用 POST 方式。Header中内容类型为JSON，即设置 "Content-Type":"application/json"。
###### Parameters
*cusCiqNum*
>关捡关联号，长度21位
>如：0150515514400001

*message*
>上传的报文的内容

###### Return Value
返回一个 *MsgReceipt* 对象，包含以下字段

*Status*
>状态编码，为以下值之一
>0 正常
>1 错误，错误消息参见 Message

 *TaskId*
>任务编号

 *DateReceived*
> 报文接收日期

 *Message*
> 错误消息内容

###### Examples
```JSON
{"cusCiqNum":"1150515514400001","message":"..."}
```

_ _ _

### 获取报关单处理状态回执
```bash
http://183.63.251.70/SingleWindow/SingleWindowMessageService.svc/json/{taskId}
```
###### Remarks
http协议使用 GET 方式
###### Parameters
*taskId*
> 任务Id，上传报关单时返回，长度29位
> 如：T1907843510020150514f4ff60bae

###### Return Value
一个 *CusReceipt*  数组，每个CusReceipt对象包含以下字段

*MessageId*
> 回执编号

*DateCreated*
>回执生成时间

*TaskId*
> 任务编号

*ReturnType*
>返回类型,为 TCS 或 QP

*ReturnCode*
>返回代码

*ReturnInfo*
>返回信息

*EntryNo*
> 报关单号

*EportNo*
>平台编号，没有预录入号的时候用

###### Examples

发送 http 请求
```bash
curl http://183.63.251.70/SingleWindow/SingleWindowMessageService.svc/json/T1907843510020150514f4ff60ba0
```
回复如下
```JSON
[{
    "DateCreated": "2015-05-14T00:52:02.6670000Z",
    "EntryNo": "",
    "EportNo": "",
    "MessageId": "282491",
    "ReturnCode": "0",
    "ReturnInfo": "任务编号[T1907843510020150514f4ff60ba0]检验成功",
    "ReturnType": "TCS",
    "TaskId": "T1907843510020150514f4ff60ba0"
}, {
    "DateCreated": "2015-05-15T06:23:55.3400000Z",
    "EntryNo": "515420151545004662",
    "EportNo": "000000001023111528",
    "MessageId": "284480",
    "ReturnCode": "011",
    "ReturnInfo": "成功入海关预录入库;",
    "ReturnType": "QP",
    "TaskId": "T1907843510020150514f4ff60ba0"
}]
```

* * *

soap 协议
----------

### 获取接口配置

#### .net
在VS开发者工具中，打开开发者命令行，使用svcutil工具生成接口
```bash
svcutil.exe http://183.63.251.70/SingleWindow/SingleWindowMessageService.svc?wsdl
```

#### python
使用 suds (suds-jurko) 或其他 web service 库
```python
from suds.client import Client
url = "http://183.63.251.70/SingleWindow/SingleWindowMessageService.svc?wsdl"
client = Client(url)
print client
```

_ _ _

###获取关检关联号 GetCusCiqNum 方法
此方法用于获取一个关捡关联号。
##### Syntax
```cs
public string GetCusCiqNum(
	string ieFlag,
    string locationCode
)
```
###### Parameters
*ieFlag*
> Type: System.String
> 进出口标志。0为出口，1为进口

*locationCode*
> Type: System.String
> 4位长度关区代码

###### Return Value
Type: System.String
系统分配的关捡关联号


###上传报关单数据 PostMessage 方法
此方法用于上传报文到TCS
#### Syntax
```cs
public MsgReceipt PostMessage(
	string cusCiqNum,
    string message
)
```
###### Parameters
*cusCiqNum*
> Type: System.String
> 系统分派的关捡关联号

*message*
> Type: System.String
> 报关报文

###### Return Value
Type: MsgReceipt
Namespace: GZCustoms.Application.SGY.SingleWindow.MessageService.Entities
上传报文得到的回执

_ _ _

###上传报关单回执 MsgReceipt 类
上传报关报文得到的回执。

**Namespace:** GZCustoms.Application.SGY.SingleWindow.MessageService.Entities

#### Fields
| Name | Type| Description |
|------|-----|---------|
| Status | System.String | 上传报文状态。"000"表示正常,"001"表示异常 |
| TaskId | System.String | 任务Id，长29位。用于跟踪该报关单在海关处理状态 |
| DateReceived | System.String | 任务日期，ISO8601格式。 |
| Message | System.String | 错误状态说明。 |


_ _ _

### 获取报关单处理状态回执 GetCusReceipt 方法
此方法用于根据任务ID获取当前报关单处理状态信息。

#### Syntax
```cs
public IEnumerable<CusReturn> GetCusReceipt(
	string taskId
)
```

###### Parameters
*taskId*
> Type: System.String
> 任务ID，从上传报关单数据的回执中获得。

###### Return Value
Type: IEnumerable&lt;CusReturn&gt;
CusReturn的枚举类型

_ _ _

### 报关状态回执 CusReturn 类
此类型表示从TCS或者QP返回的报关单在海关的处理的状态

#### Fields
| Name | Type | Description |
|-----|-------|-------------|
| TaskId | System.String | 任务Id |
| ReturnType | System.String | 返回类型，可能值为TCS, QP。表明处理信息是TCS或QP返回的。 |
| ReturnCode | System.String | 返回状态代码。 |
| ReturnInfo | System.String | 处理状态的说明 |
| EntryNo | System.String | 报关单号。在还没有分派报关单号的时候，值为 null。 |
| EportNo | System.String | 平台编号 |
| MessageId | System.String | 当前消息的编号，顺序增长 |
| DateCreated | System.String | 当前消息生成时间，ISO8601格式。 |

* * *

上传报关单报文格式
-----

报文Schema

```xml
<!-- edited with XMLSpy v2011 (http://www.altova.com) by paddy (custom) -->
<xs:schema xmlns:xs="http://www.w3.org/2001/XMLSchema" elementFormDefault="qualified" attributeFormDefault="unqualified">
  <xs:element name="DeclEnvelop">
    <xs:annotation>
      <xs:documentation>Comment describing your root element</xs:documentation>
    </xs:annotation>
    <xs:complexType>
      <xs:sequence>
        <xs:element name="EnvelopHead">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="Name">
                <xs:annotation>
                  <xs:documentation>报文名</xs:documentation>
                </xs:annotation>
              </xs:element>
              <xs:element name="Version">
                <xs:annotation>
                  <xs:documentation>报文版本</xs:documentation>
                </xs:annotation>
              </xs:element>
              <xs:element name="From">
                <xs:annotation>
                  <xs:documentation>发送方</xs:documentation>
                </xs:annotation>
              </xs:element>
              <xs:element name="To">
                <xs:annotation>
                  <xs:documentation>接收方</xs:documentation>
                </xs:annotation>
              </xs:element>
              <xs:element name="Operation">
                <xs:annotation>
                  <xs:documentation>操作类型（1为暂存，2为申报/上载）</xs:documentation>
                </xs:annotation>
              </xs:element>
              <xs:element name="SendTime">
                <xs:annotation>
                  <xs:documentation>发送时间</xs:documentation>
                </xs:annotation>
              </xs:element>
              <xs:element name="Guid"/>
            </xs:sequence>
          </xs:complexType>
        </xs:element>
        <xs:element name="EnvelopBody">
          <xs:complexType>
            <xs:sequence>
              <xs:element name="DECL_HEAD">
                <xs:complexType>
                  <xs:sequence>
                    <xs:element name="ENTRY_ID" type="xs:string" minOccurs="0"/>
                    <xs:element name="PRE_ENTRY_ID" nillable="false">
                      <xs:annotation>
                        <xs:documentation>预录入号</xs:documentation>
                      </xs:annotation>
                      <xs:simpleType>
                        <xs:restriction base="xs:string">
                          <xs:maxLength value="18"/>
                        </xs:restriction>
                      </xs:simpleType>
                    </xs:element>
                    <xs:element name="DECL_PORT" nillable="false">
                      <xs:annotation>
                        <xs:documentation>申报口岸</xs:documentation>
                      </xs:annotation>
                      <xs:simpleType>
                        <xs:restriction base="xs:string">
                          <xs:length value="4"/>
                        </xs:restriction>
                      </xs:simpleType>
                    </xs:element>
                    <xs:element name="I_E_PORT" nillable="false">
                      <xs:annotation>
                        <xs:documentation>进口口岸</xs:documentation>
                      </xs:annotation>
                      <xs:simpleType>
                        <xs:restriction base="xs:string">
                          <xs:length value="4"/>
                        </xs:restriction>
                      </xs:simpleType>
                    </xs:element>
                    <xs:element name="ENTRY_TYPE" type="xs:string">
                      <xs:annotation>
                        <xs:documentation>报关单类型</xs:documentation>
                      </xs:annotation>
                    </xs:element>
                    <xs:element name="ENTRY_TRANSIT_TYPE" type="xs:string">
                    <xs:annotation>
                      <xs:documentation>转关提前报关; 003表示转关单;001表示普通报关单</xs:documentation>
                    </xs:annotation>
                    </xs:element>
                    <xs:element name="MANUAL_NO" nillable="true">
                      <xs:annotation>
                        <xs:documentation>备案号</xs:documentation>
                      </xs:annotation>
                      <xs:simpleType>
                        <xs:restriction base="xs:string">
                          <xs:length value="12"/>
                        </xs:restriction>
                      </xs:simpleType>
                    </xs:element>
                    <xs:element name="CONTR_NO" nillable="true">
                      <xs:annotation>
                        <xs:documentation>合同协议号</xs:documentation>
                      </xs:annotation>
                      <xs:simpleType>
                        <xs:restriction base="xs:string">
                          <xs:maxLength value="32"/>
                        </xs:restriction>
                      </xs:simpleType>
                    </xs:element>
                    <xs:element name="I_E_FLAG" nillable="false">
                      <xs:annotation>
                        <xs:documentation>进出口类型</xs:documentation>
                      </xs:annotation>
                      <xs:simpleType>
                        <xs:restriction base="xs:string">
                          <xs:length value="1"/>
                        </xs:restriction>
                      </xs:simpleType>
                    </xs:element>
                    <xs:element name="I_E_DATE" nillable="true">
                      <xs:annotation>
                        <xs:documentation>进出口日期</xs:documentation>
                      </xs:annotation>
                      <xs:simpleType>
                        <xs:restriction base="xs:string">
                          <xs:length value="10"/>
                        </xs:restriction>
                      </xs:simpleType>
                    </xs:element>
                    <xs:element name="D_DATE" nillable="true">
                      <xs:annotation>
                        <xs:documentation>传输日期</xs:documentation>
                      </xs:annotation>
                      <xs:simpleType>
                        <xs:restriction base="xs:string">
                          <xs:length value="10"/>
                        </xs:restriction>
                      </xs:simpleType>
                    </xs:element>
                    <xs:element name="MOD_NUM" minOccurs="0">
                      <xs:simpleType>
                        <xs:restriction base="xs:decimal"/>
                      </xs:simpleType>
                    </xs:element>
                    <xs:element name="AGENT_CODE" nillable="false">
                      <xs:annotation>
                        <xs:documentation>申报单位编码</xs:documentation>
                      </xs:annotation>
                      <xs:simpleType>
                        <xs:restriction base="xs:string">
                          <xs:length value="10"/>
                        </xs:restriction>
                      </xs:simpleType>
                    </xs:element>
                    <xs:element name="AGENT_NAME" nillable="false">
                      <xs:annotation>
                        <xs:documentation>申报单位名称</xs:documentation>
                      </xs:annotation>
                      <xs:simpleType>
                        <xs:restriction base="xs:string">
                          <xs:maxLength value="70"/>
                        </xs:restriction>
                      </xs:simpleType>
                    </xs:element>
                    <xs:element name="OWNER_CODE" nillable="false">
                      <xs:annotation>
                        <xs:documentation>收货单位编码</xs:documentation>
                      </xs:annotation>
                      <xs:simpleType>
                        <xs:restriction base="xs:string">
                          <xs:length value="10"/>
                        </xs:restriction>
                      </xs:simpleType>
                    </xs:element>
                    <xs:element name="OWNER_NAME" nillable="false">
                      <xs:annotation>
                        <xs:documentation>收货单位名称</xs:documentation>
                      </xs:annotation>
                      <xs:simpleType>
                        <xs:restriction base="xs:string">
                          <xs:maxLength value="70"/>
                        </xs:restriction>
                      </xs:simpleType>
                    </xs:element>
                    <xs:element name="TRADE_CO" nillable="false">
                      <xs:annotation>
                        <xs:documentation>经营单位代码</xs:documentation>
                      </xs:annotation>
                      <xs:simpleType>
                        <xs:restriction base="xs:string">
                          <xs:length value="10"/>
                        </xs:restriction>
                      </xs:simpleType>
                    </xs:element>
                    <xs:element name="TRADE_NAME" nillable="false">
                      <xs:annotation>
                        <xs:documentation>经营单位名称</xs:documentation>
                      </xs:annotation>
                      <xs:simpleType>
                        <xs:restriction base="xs:string">
                          <xs:maxLength value="70"/>
                        </xs:restriction>
                      </xs:simpleType>
                    </xs:element>
                    <xs:element name="CO_OWNER" type="xs:string" minOccurs="0"/>
                    <xs:element name="TRAF_MODE" type="xs:string">
                      <xs:annotation>
                        <xs:documentation>运输方式</xs:documentation>
                      </xs:annotation>
                    </xs:element>
                    <xs:element name="TRAF_NAME" nillable="true">
                      <xs:annotation>
                        <xs:documentation>运输工具名称</xs:documentation>
                      </xs:annotation>
                      <xs:simpleType>
                        <xs:restriction base="xs:string">
                          <xs:maxLength value="26"/>
                        </xs:restriction>
                      </xs:simpleType>
                    </xs:element>
                    <xs:element name="BILL_NO" nillable="true">
                      <xs:annotation>
                        <xs:documentation>提单号</xs:documentation>
                      </xs:annotation>
                      <xs:simpleType>
                        <xs:restriction base="xs:string">
                          <xs:maxLength value="32"/>
                        </xs:restriction>
                      </xs:simpleType>
                    </xs:element>
                    <xs:element name="TRADE_MODE" nillable="true">
                      <xs:annotation>
                        <xs:documentation>监管方式</xs:documentation>
                      </xs:annotation>
                      <xs:simpleType>
                        <xs:restriction base="xs:string">
                          <xs:length value="4"/>
                        </xs:restriction>
                      </xs:simpleType>
                    </xs:element>
                    <xs:element name="VOYAGE_NO" nillable="true">
                      <xs:annotation>
                        <xs:documentation>航次号</xs:documentation>
                      </xs:annotation>
                      <xs:simpleType>
                        <xs:restriction base="xs:string">
                          <xs:maxLength value="32"/>
                        </xs:restriction>
                      </xs:simpleType>
                    </xs:element>
                    <xs:element name="CUT_MODE" nillable="true">
                      <xs:annotation>
                        <xs:documentation>征免性质</xs:documentation>
                      </xs:annotation>
                      <xs:simpleType>
                        <xs:restriction base="xs:string">
                          <xs:length value="3"/>
                        </xs:restriction>
                      </xs:simpleType>
                    </xs:element>
                    <xs:element name="PAY_MODE" nillable="true">
                      <xs:annotation>
                        <xs:documentation>收结汇方式</xs:documentation>
                      </xs:annotation>
                      <xs:simpleType>
                        <xs:restriction base="xs:string">
                          <xs:length value="1"/>
                        </xs:restriction>
                      </xs:simpleType>
                    </xs:element>
                    <xs:element name="TRADE_COUNTRY" nillable="false">
                      <xs:annotation>
                        <xs:documentation>抵运国</xs:documentation>
                      </xs:annotation>
                      <xs:simpleType>
                        <xs:restriction base="xs:string">
                          <xs:maxLength value="5"/>
                        </xs:restriction>
                      </xs:simpleType>
                    </xs:element>
                    <xs:element name="SERVICE_FEE" type="xs:decimal" nillable="true" minOccurs="0"/>
                    <xs:element name="PAYMENT_MARK" nillable="true">
                      <xs:annotation>
                        <xs:documentation>纳税单位</xs:documentation>
                      </xs:annotation>
                      <xs:simpleType>
                        <xs:restriction base="xs:string">
                          <xs:length value="3"/>
                        </xs:restriction>
                      </xs:simpleType>
                    </xs:element>
                    <xs:element name="LICENSE_NO" nillable="true">
                      <xs:annotation>
                        <xs:documentation>许可证编号</xs:documentation>
                      </xs:annotation>
                      <xs:simpleType>
                        <xs:restriction base="xs:string">
                          <xs:maxLength value="20"/>
                        </xs:restriction>
                      </xs:simpleType>
                    </xs:element>
                    <xs:element name="DESTINATION_PORT">
                      <xs:annotation>
                        <xs:documentation>抵运港</xs:documentation>
                      </xs:annotation>
                      <xs:simpleType>
                        <xs:restriction base="xs:string"/>
                      </xs:simpleType>
                    </xs:element>
                    <xs:element name="DESTINATION_CODE" nillable="true">
                      <xs:annotation>
                        <xs:documentation>境内目的地</xs:documentation>
                      </xs:annotation>
                      <xs:simpleType>
                        <xs:restriction base="xs:string">
                          <xs:length value="5"/>
                        </xs:restriction>
                      </xs:simpleType>
                    </xs:element>
                    <xs:element name="TRANS_MODE" nillable="true">
                      <xs:annotation>
                        <xs:documentation>成效方式</xs:documentation>
                      </xs:annotation>
                      <xs:simpleType>
                        <xs:restriction base="xs:string">
                          <xs:length value="1"/>
                        </xs:restriction>
                      </xs:simpleType>
                    </xs:element>
                    <xs:element name="PACK_NO" type="xs:decimal" nillable="true">
                      <xs:annotation>
                        <xs:documentation>件数</xs:documentation>
                      </xs:annotation>
                    </xs:element>
                    <xs:element name="GROSS_WT" type="xs:decimal" nillable="true">
                      <xs:annotation>
                        <xs:documentation>毛重</xs:documentation>
                      </xs:annotation>
                    </xs:element>
                    <xs:element name="NET_WT" type="xs:decimal" nillable="true">
                      <xs:annotation>
                        <xs:documentation>净重</xs:documentation>
                      </xs:annotation>
                    </xs:element>
                    <xs:element name="WRAP_TYPE" nillable="true">
                      <xs:annotation>
                        <xs:documentation>包装种类</xs:documentation>
                      </xs:annotation>
                      <xs:simpleType>
                        <xs:restriction base="xs:string">
                          <xs:length value="1"/>
                        </xs:restriction>
                      </xs:simpleType>
                    </xs:element>
                    <xs:element name="FEE_CURR" nillable="true">
                      <xs:annotation>
                        <xs:documentation>运费币制</xs:documentation>
                      </xs:annotation>
                      <xs:simpleType>
                        <xs:restriction base="xs:string">
                          <xs:length value="3"/>
                        </xs:restriction>
                      </xs:simpleType>
                    </xs:element>
                    <xs:element name="FEE_MARK" nillable="true">
                      <xs:annotation>
                        <xs:documentation>运费标记</xs:documentation>
                      </xs:annotation>
                      <xs:simpleType>
                        <xs:restriction base="xs:string">
                          <xs:length value="1"/>
                        </xs:restriction>
                      </xs:simpleType>
                    </xs:element>
                    <xs:element name="FEE_RATE" type="xs:decimal" nillable="true">
                      <xs:annotation>
                        <xs:documentation>运费/率</xs:documentation>
                      </xs:annotation>
                    </xs:element>
                    <xs:element name="INSUR_CURR" nillable="true">
                      <xs:annotation>
                        <xs:documentation>保费币制</xs:documentation>
                      </xs:annotation>
                      <xs:simpleType>
                        <xs:restriction base="xs:string">
                          <xs:length value="3"/>
                        </xs:restriction>
                      </xs:simpleType>
                    </xs:element>
                    <xs:element name="INSUR_MARK" nillable="true">
                      <xs:annotation>
                        <xs:documentation>保费标记</xs:documentation>
                      </xs:annotation>
                      <xs:simpleType>
                        <xs:restriction base="xs:string">
                          <xs:length value="1"/>
                        </xs:restriction>
                      </xs:simpleType>
                    </xs:element>
                    <xs:element name="INSUR_RATE" type="xs:decimal" nillable="true">
                      <xs:annotation>
                        <xs:documentation>保费/率</xs:documentation>
                      </xs:annotation>
                    </xs:element>
                    <xs:element name="OTHER_CURR" nillable="true">
                      <xs:annotation>
                        <xs:documentation>杂费币制</xs:documentation>
                      </xs:annotation>
                      <xs:simpleType>
                        <xs:restriction base="xs:string">
                          <xs:length value="3"/>
                        </xs:restriction>
                      </xs:simpleType>
                    </xs:element>
                    <xs:element name="OTHER_MARK" nillable="true">
                      <xs:annotation>
                        <xs:documentation>杂费标记</xs:documentation>
                      </xs:annotation>
                      <xs:simpleType>
                        <xs:restriction base="xs:string">
                          <xs:length value="1"/>
                        </xs:restriction>
                      </xs:simpleType>
                    </xs:element>
                    <xs:element name="OTHER_RATE" type="xs:decimal" nillable="true">
                      <xs:annotation>
                        <xs:documentation>杂费/率</xs:documentation>
                      </xs:annotation>
                    </xs:element>
                    <xs:element name="IN_RATIO" type="xs:decimal" nillable="true">
                      <xs:annotation>
                        <xs:documentation>内销比率</xs:documentation>
                      </xs:annotation>
                    </xs:element>
                    <xs:element name="APPR_NO" nillable="true">
                      <xs:annotation>
                        <xs:documentation>批准文号</xs:documentation>
                      </xs:annotation>
                      <xs:simpleType>
                        <xs:restriction base="xs:string">
                          <xs:maxLength value="30"/>
                        </xs:restriction>
                      </xs:simpleType>
                    </xs:element>
                    <xs:element name="RELATIVE_ID" nillable="true">
                      <xs:annotation>
                        <xs:documentation>关联报关单号</xs:documentation>
                      </xs:annotation>
                      <xs:simpleType>
                        <xs:restriction base="xs:string">
                          <xs:length value="18"/>
                        </xs:restriction>
                      </xs:simpleType>
                    </xs:element>
                    <xs:element name="RELATIVE_MANUAL_NO" nillable="true">
                      <xs:annotation>
                        <xs:documentation>关联备案号</xs:documentation>
                      </xs:annotation>
                      <xs:simpleType>
                        <xs:restriction base="xs:string">
                          <xs:length value="12"/>
                        </xs:restriction>
                      </xs:simpleType>
                    </xs:element>
                    <xs:element name="BONDED_NO" nillable="true">
                      <xs:annotation>
                        <xs:documentation>监管仓号</xs:documentation>
                      </xs:annotation>
                      <xs:simpleType>
                        <xs:restriction base="xs:string">
                          <xs:maxLength value="32"/>
                        </xs:restriction>
                      </xs:simpleType>
                    </xs:element>
                    <xs:element name="CUSTOMS_FIELD" nillable="true">
                      <xs:annotation>
                        <xs:documentation>货场代码</xs:documentation>
                      </xs:annotation>
                      <xs:simpleType>
                        <xs:restriction base="xs:string">
                          <xs:maxLength value="8"/>
                        </xs:restriction>
                      </xs:simpleType>
                    </xs:element>
                    <xs:element name="EDI_ID" type="xs:string" minOccurs="0"/>
                    <xs:element name="EDI_REMARK" type="xs:string" minOccurs="0"/>
                    <xs:element name="PRTENER_ID" type="xs:string" minOccurs="0"/>
                    <xs:element name="P_DATE" type="xs:dateTime" nillable="true" minOccurs="0"/>
                    <xs:element name="TYPIST_NO" nillable="false">
                      <xs:annotation>
                        <xs:documentation>录入员IC卡号</xs:documentation>
                      </xs:annotation>
                      <xs:simpleType>
                        <xs:restriction base="xs:string">
                          <xs:length value="13"/>
                        </xs:restriction>
                      </xs:simpleType>
                    </xs:element>
                    <xs:element name="TYPIST_NAME" nillable="false">
                      <xs:annotation>
                        <xs:documentation>录入员姓名</xs:documentation>
                      </xs:annotation>
                      <xs:simpleType>
                        <xs:restriction base="xs:string">
                          <xs:maxLength value="70"/>
                        </xs:restriction>
                      </xs:simpleType>
                    </xs:element>
                    <xs:element name="DECLARE_NO" type="xs:string" minOccurs="0"/>
                    <xs:element name="BP_NO" type="xs:string" minOccurs="0"/>
                    <xs:element name="COP_CODE">
                      <xs:annotation>
                        <xs:documentation>录入单位组织机构代码</xs:documentation>
                      </xs:annotation>
                      <xs:simpleType>
                        <xs:restriction base="xs:string">
                          <xs:length value="9"/>
                        </xs:restriction>
                      </xs:simpleType>
                    </xs:element>
                    <xs:element name="COP_NAME">
                      <xs:annotation>
                        <xs:documentation>录入单位名称</xs:documentation>
                      </xs:annotation>
                      <xs:simpleType>
                        <xs:restriction base="xs:string">
                          <xs:maxLength value="70"/>
                        </xs:restriction>
                      </xs:simpleType>
                    </xs:element>
                    <xs:element name="NOTE_S" nillable="true">
                      <xs:annotation>
                        <xs:documentation>报关备注</xs:documentation>
                      </xs:annotation>
                      <xs:simpleType>
                        <xs:restriction base="xs:string">
                          <xs:maxLength value="255"/>
                        </xs:restriction>
                      </xs:simpleType>
                    </xs:element>
                  </xs:sequence>
                </xs:complexType>
              </xs:element>
              <xs:element name="DECL_LISTS">
                <xs:complexType>
                  <xs:sequence maxOccurs="unbounded">
                    <xs:element name="DECL_LIST">
                      <xs:complexType>
                        <xs:sequence>
                          <xs:element name="ENTRY_ID" type="xs:string" minOccurs="0"/>
                          <xs:element name="PRE_ENTRY_ID" type="xs:string" minOccurs="0">
                            <xs:annotation>
                              <xs:documentation>预录入号</xs:documentation>
                            </xs:annotation>
                          </xs:element>
                          <xs:element name="G_NO" nillable="false">
                            <xs:annotation>
                              <xs:documentation>商品序号</xs:documentation>
                            </xs:annotation>
                            <xs:simpleType>
                              <xs:restriction base="xs:string">
                                <xs:length value="9"/>
                              </xs:restriction>
                            </xs:simpleType>
                          </xs:element>
                          <xs:element name="PRE_ENTRY_ID" type="xs:string">
                            <xs:annotation>
                              <xs:documentation>预录入号</xs:documentation>
                            </xs:annotation>
                          </xs:element>
                          <xs:element name="CODE_TS" nillable="false">
                            <xs:annotation>
                              <xs:documentation>商品编码</xs:documentation>
                            </xs:annotation>
                            <xs:simpleType>
                              <xs:restriction base="xs:string">
                                <xs:length value="10"/>
                              </xs:restriction>
                            </xs:simpleType>
                          </xs:element>
                          <xs:element name="CLASS_MARK" nillable="true">
                            <xs:annotation>
                              <xs:documentation>归类标志</xs:documentation>
                            </xs:annotation>
                            <xs:simpleType>
                              <xs:restriction base="xs:string">
                                <xs:length value="1"/>
                              </xs:restriction>
                            </xs:simpleType>
                          </xs:element>
                          <xs:element name="G_NAME" nillable="true">
                            <xs:annotation>
                              <xs:documentation>中文品名</xs:documentation>
                            </xs:annotation>
                            <xs:simpleType>
                              <xs:restriction base="xs:string">
                                <xs:maxLength value="255"/>
                              </xs:restriction>
                            </xs:simpleType>
                          </xs:element>
                          <xs:element name="G_MODEL" nillable="true">
                            <xs:annotation>
                              <xs:documentation>规格型号</xs:documentation>
                            </xs:annotation>
                            <xs:simpleType>
                              <xs:restriction base="xs:string">
                                <xs:maxLength value="255"/>
                              </xs:restriction>
                            </xs:simpleType>
                          </xs:element>
                          <xs:element name="ORIGIN_COUNTRY" nillable="true">
                            <xs:annotation>
                              <xs:documentation>原产地</xs:documentation>
                            </xs:annotation>
                            <xs:simpleType>
                              <xs:restriction base="xs:string">
                                <xs:length value="3"/>
                              </xs:restriction>
                            </xs:simpleType>
                          </xs:element>
                          <xs:element name="CONTR_ITEM" type="xs:decimal" nillable="true">
                            <xs:annotation>
                              <xs:documentation>备案序号</xs:documentation>
                            </xs:annotation>
                          </xs:element>
                          <xs:element name="G_QTY" type="xs:decimal" nillable="true">
                            <xs:annotation>
                              <xs:documentation>成效数量</xs:documentation>
                            </xs:annotation>
                          </xs:element>
                          <xs:element name="G_UNIT" nillable="true">
                            <xs:annotation>
                              <xs:documentation>成效单位</xs:documentation>
                            </xs:annotation>
                            <xs:simpleType>
                              <xs:restriction base="xs:string">
                                <xs:length value="3"/>
                              </xs:restriction>
                            </xs:simpleType>
                          </xs:element>
                          <xs:element name="QTY_1" type="xs:decimal" nillable="true">
                            <xs:annotation>
                              <xs:documentation>第一法定数量</xs:documentation>
                            </xs:annotation>
                          </xs:element>
                          <xs:element name="UNIT_1" nillable="true">
                            <xs:annotation>
                              <xs:documentation>第一法定单位</xs:documentation>
                            </xs:annotation>
                            <xs:simpleType>
                              <xs:restriction base="xs:string">
                                <xs:length value="3"/>
                              </xs:restriction>
                            </xs:simpleType>
                          </xs:element>
                          <xs:element name="QTY_2" type="xs:decimal" nillable="true">
                            <xs:annotation>
                              <xs:documentation>第二法定数量</xs:documentation>
                            </xs:annotation>
                          </xs:element>
                          <xs:element name="UNIT_2" nillable="true">
                            <xs:annotation>
                              <xs:documentation>第二法定单位</xs:documentation>
                            </xs:annotation>
                            <xs:simpleType>
                              <xs:restriction base="xs:string">
                                <xs:length value="3"/>
                              </xs:restriction>
                            </xs:simpleType>
                          </xs:element>
                          <xs:element name="TRADE_CURR" nillable="true">
                            <xs:annotation>
                              <xs:documentation>币制</xs:documentation>
                            </xs:annotation>
                            <xs:simpleType>
                              <xs:restriction base="xs:string">
                                <xs:length value="3"/>
                              </xs:restriction>
                            </xs:simpleType>
                          </xs:element>
                          <xs:element name="DECL_PRICE" type="xs:decimal" nillable="true">
                            <xs:annotation>
                              <xs:documentation>单价</xs:documentation>
                            </xs:annotation>
                          </xs:element>
                          <xs:element name="DECL_TOTAL" type="xs:decimal" nillable="true">
                            <xs:annotation>
                              <xs:documentation>总价</xs:documentation>
                            </xs:annotation>
                          </xs:element>
                          <xs:element name="USE_TO" nillable="true">
                            <xs:annotation>
                              <xs:documentation>用途</xs:documentation>
                            </xs:annotation>
                            <xs:simpleType>
                              <xs:restriction base="xs:string">
                                <xs:maxLength value="2"/>
                              </xs:restriction>
                            </xs:simpleType>
                          </xs:element>
                          <xs:element name="DUTY_MODE" nillable="true">
                            <xs:annotation>
                              <xs:documentation>证免方式</xs:documentation>
                            </xs:annotation>
                            <xs:simpleType>
                              <xs:restriction base="xs:string">
                                <xs:length value="1"/>
                              </xs:restriction>
                            </xs:simpleType>
                          </xs:element>
                          <xs:element name="WORK_USD" type="xs:decimal" nillable="true">
                            <xs:annotation>
                              <xs:documentation>工缴费</xs:documentation>
                            </xs:annotation>
                          </xs:element>
                          <xs:element name="PRDT_NO" nillable="true">
                            <xs:annotation>
                              <xs:documentation>版本号</xs:documentation>
                            </xs:annotation>
                            <xs:simpleType>
                              <xs:restriction base="xs:string">
                                <xs:maxLength value="8"/>
                              </xs:restriction>
                            </xs:simpleType>
                          </xs:element>
                          <xs:element name="GOODS_ID" nillable="true">
                            <xs:annotation>
                              <xs:documentation>货号</xs:documentation>
                            </xs:annotation>
                            <xs:simpleType>
                              <xs:restriction base="xs:string">
                                <xs:maxLength value="30"/>
                              </xs:restriction>
                            </xs:simpleType>
                          </xs:element>
                          <xs:element name="NOTE_S" nillable="true">
                            <xs:annotation>
                              <xs:documentation>备注</xs:documentation>
                            </xs:annotation>
                            <xs:simpleType>
                              <xs:restriction base="xs:string">
                                <xs:maxLength value="255"/>
                              </xs:restriction>
                            </xs:simpleType>
                          </xs:element>
                        </xs:sequence>
                      </xs:complexType>
                    </xs:element>
                  </xs:sequence>
                </xs:complexType>
              </xs:element>
              <xs:element name="DECL_CONTAINERS">
                <xs:complexType>
                  <xs:sequence>
                    <xs:element name="DECL_CONTAINER" minOccurs="0" maxOccurs="unbounded">
                      <xs:complexType>
                        <xs:sequence>
                          <xs:element name="ENTRY_ID" type="xs:string" minOccurs="0"/>
                          <xs:element name="PRE_ENTRY_ID" type="xs:string" minOccurs="0">
                            <xs:annotation>
                              <xs:documentation>预录入号</xs:documentation>
                            </xs:annotation>
                          </xs:element>
                          <xs:element name="CONTAINER_ID" nillable="false">
                            <xs:annotation>
                              <xs:documentation>集装箱号</xs:documentation>
                            </xs:annotation>
                            <xs:simpleType>
                              <xs:restriction base="xs:string">
                                <xs:maxLength value="11"/>
                              </xs:restriction>
                            </xs:simpleType>
                          </xs:element>
                          <xs:element name="CONTAINER_MD" nillable="true">
                            <xs:annotation>
                              <xs:documentation>集装箱规格</xs:documentation>
                            </xs:annotation>
                            <xs:simpleType>
                              <xs:restriction base="xs:string">
                                <xs:length value="1"/>
                              </xs:restriction>
                            </xs:simpleType>
                          </xs:element>
                          <xs:element name="CONTAINER_WT" type="xs:decimal">
                            <xs:annotation>
                              <xs:documentation>集装箱自重</xs:documentation>
                            </xs:annotation>
                          </xs:element>
                          <xs:element name="NOTE_S" type="xs:string" minOccurs="0">
                            <xs:annotation>
                              <xs:documentation>报关备注</xs:documentation>
                            </xs:annotation>
                          </xs:element>
                        </xs:sequence>
                      </xs:complexType>
                    </xs:element>
                  </xs:sequence>
                </xs:complexType>
              </xs:element>
              <xs:element name="DECL_CERTIFICATES">
                <xs:complexType>
                  <xs:sequence>
                    <xs:element name="DECL_CERTIFICATE" minOccurs="0" maxOccurs="unbounded">
                      <xs:complexType>
                        <xs:sequence>
                          <xs:element name="ENTRY_ID" type="xs:string" minOccurs="0"/>
                          <xs:element name="PRE_ENTRY_ID" type="xs:string" minOccurs="0">
                            <xs:annotation>
                              <xs:documentation>预录入号</xs:documentation>
                            </xs:annotation>
                          </xs:element>
                          <xs:element name="DOCU_CODE" nillable="false">
                            <xs:annotation>
                              <xs:documentation>随附单证代码</xs:documentation>
                            </xs:annotation>
                            <xs:simpleType>
                              <xs:restriction base="xs:string">
                                <xs:length value="1"/>
                              </xs:restriction>
                            </xs:simpleType>
                          </xs:element>
                          <xs:element name="CERT_CODE" nillable="true">
                            <xs:annotation>
                              <xs:documentation>随附单证编号(通关单号)</xs:documentation>
                            </xs:annotation>
                            <xs:simpleType>
                              <xs:restriction base="xs:string">
                                <xs:length value="32"/>
                              </xs:restriction>
                            </xs:simpleType>
                          </xs:element>
                        </xs:sequence>
                      </xs:complexType>
                    </xs:element>
                  </xs:sequence>
                </xs:complexType>
              </xs:element>
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:sequence>
    </xs:complexType>
  </xs:element>
</xs:schema>

```

示例报文
```xml
<DeclEnvelop>
	<EnvelopHead>
		<Name>九城报关</Name>
		<Version>1.0</Version>
		<From>九城测试客户端</From>
		<To>广州海关</To>
		<Operation>2</Operation>
		<SendTime>2015-6-19 17:43:27</SendTime>
		<Guid>9C703D79B15B49C38298BD63785295D7</Guid>
	</EnvelopHead>
	<EnvelopBody>
		<DECL_HEAD>
			<PRE_ENTRY_ID />
			<DECL_PORT>5158</DECL_PORT>
			<I_E_PORT>5158</I_E_PORT>
			<ENTRY_TYPE>O</ENTRY_TYPE>
			<MANUAL_NO />
			<CONTR_NO>2015SPCCSL</CONTR_NO>
			<I_E_FLAG>I</I_E_FLAG>
			<I_E_DATE>2015-06-19</I_E_DATE>
			<D_DATE>2015-06-19</D_DATE>
			<MOD_NUM />
			<AGENT_CODE>4422980007</AGENT_CODE>
			<AGENT_NAME>佛山市顺德区勒流镇报关有限公司</AGENT_NAME>
			<OWNER_CODE>4406960904</OWNER_CODE>
			<OWNER_NAME>佛山市天雅进出口有限公司</OWNER_NAME>
			<TRADE_CO>4406960904</TRADE_CO>
			<TRADE_NAME>佛山市天雅进出口有限公司</TRADE_NAME>
			<CO_OWNER>6</CO_OWNER>
			<TRAF_MODE>2</TRAF_MODE>
			<TRAF_NAME>5200500185</TRAF_NAME>
			<BILL_NO>WHL15236052</BILL_NO>
			<TRADE_MODE>0110</TRADE_MODE>
			<VOYAGE_NO>515401506140</VOYAGE_NO>
			<CUT_MODE>101</CUT_MODE>
			<PAY_MODE />
			<SERVICE_FEE />
			<TRADE_COUNTRY>136</TRADE_COUNTRY>
			<PAYMENT_MARK>001</PAYMENT_MARK>
			<LICENSE_NO />
			<DESTINATION_PORT>110</DESTINATION_PORT>
			<DESTINATION_CODE>44069</DESTINATION_CODE>
			<TRANS_MODE>1</TRANS_MODE>
			<PACK_NO>259</PACK_NO>
			<GROSS_WT>210000.00000</GROSS_WT>
			<NET_WT>210000.00000</NET_WT>
			<WRAP_TYPE>7</WRAP_TYPE>
			<FEE_CURR />
			<FEE_MARK />
			<FEE_RATE />
			<INSUR_CURR />
			<INSUR_MARK />
			<INSUR_RATE />
			<OTHER_CURR />
			<OTHER_MARK />
			<OTHER_RATE />
			<IN_RATIO />
			<APPR_NO />
			<RELATIVE_ID />
			<RELATIVE_MANUAL_NO />
			<BONDED_NO />
			<CUSTOMS_FIELD>5158</CUSTOMS_FIELD>
			<EDI_ID />
			<EDI_REMARK />
			<PARTENER_ID />
			<P_DATE />
			<TYPIST_NO>9200000005321</TYPIST_NO>
			<TYPIST_NAME>凌志铭</TYPIST_NAME>
			<DECLARE_NO />
			<BP_NO />
			<NOTE_S>顺德勒流</NOTE_S>
			<COP_CODE>190784351</COP_CODE>
			<COP_NAME>广州市海通科技服务有限公司</COP_NAME>
			<FILE_CHANNEL />
			<ENTRY_TRANSIT_TYPE>001</ENTRY_TRANSIT_TYPE>
		</DECL_HEAD>
		<DECL_LISTS>
			<DECL_LIST>
				<G_NO>000000001</G_NO>
				<PRE_ENTRY_ID />
				<CODE_TS>4407999099</CODE_TS>
				<CLASS_MARK />
				<G_NAME>橡胶木方</G_NAME>
				<G_MODEL />
				<ORIGIN_COUNTRY>136</ORIGIN_COUNTRY>
				<CONTR_ITEM />
				<G_QTY>281.4200</G_QTY>
				<G_UNIT>033</G_UNIT>
				<QTY_1>210000.0000</QTY_1>
				<UNIT_1>035</UNIT_1>
				<QTY_2>281.4200</QTY_2>
				<UNIT_2>033</UNIT_2>
				<TRADE_CURR>502</TRADE_CURR>
				<DECL_PRICE>311.0000</DECL_PRICE>
				<DECL_TOTAL>87521.62</DECL_TOTAL>
				<USE_TO>11</USE_TO>
				<DUTY_MODE>1</DUTY_MODE>
				<WORK_USD />
				<PRDT_NO />
				<GOODS_ID />
				<NOTE_S />
			</DECL_LIST>
		</DECL_LISTS>
		<DECL_CONTAINERS>
			<DECL_CONTAINER>
				<ORDER_NO>1</ORDER_NO>
				<PRE_ENTRY_ID />
				<ENTRY_ID />
				<CONTAINER_MD>L</CONTAINER_MD>
				<CONTAINER_WT />
				<CONTAINER_ID>TEMU8495895</CONTAINER_ID>
				<OID>1</OID>
			</DECL_CONTAINER>
			<DECL_CONTAINER>
				<ORDER_NO>2</ORDER_NO>
				<PRE_ENTRY_ID />
				<ENTRY_ID />
				<CONTAINER_MD>L</CONTAINER_MD>
				<CONTAINER_WT />
				<CONTAINER_ID>MAGU5108707</CONTAINER_ID>
				<OID>2</OID>
			</DECL_CONTAINER>
			<DECL_CONTAINER>
				<ORDER_NO>3</ORDER_NO>
				<PRE_ENTRY_ID />
				<ENTRY_ID />
				<CONTAINER_MD>L</CONTAINER_MD>
				<CONTAINER_WT />
				<CONTAINER_ID>BMOU5569352</CONTAINER_ID>
				<OID>3</OID>
			</DECL_CONTAINER>
			<DECL_CONTAINER>
				<ORDER_NO>4</ORDER_NO>
				<PRE_ENTRY_ID />
				<ENTRY_ID />
				<CONTAINER_MD>L</CONTAINER_MD>
				<CONTAINER_WT />
				<CONTAINER_ID>TCNU7967678</CONTAINER_ID>
				<OID>4</OID>
			</DECL_CONTAINER>
			<DECL_CONTAINER>
				<ORDER_NO>5</ORDER_NO>
				<PRE_ENTRY_ID />
				<ENTRY_ID />
				<CONTAINER_MD>L</CONTAINER_MD>
				<CONTAINER_WT />
				<CONTAINER_ID>BMOU5733487</CONTAINER_ID>
				<OID>5</OID>
			</DECL_CONTAINER>
			<DECL_CONTAINER>
				<ORDER_NO>6</ORDER_NO>
				<PRE_ENTRY_ID />
				<ENTRY_ID />
				<CONTAINER_MD>L</CONTAINER_MD>
				<CONTAINER_WT />
				<CONTAINER_ID>TRLU8116967</CONTAINER_ID>
				<OID>6</OID>
			</DECL_CONTAINER>
			<DECL_CONTAINER>
				<ORDER_NO>7</ORDER_NO>
				<PRE_ENTRY_ID />
				<ENTRY_ID />
				<CONTAINER_MD>L</CONTAINER_MD>
				<CONTAINER_WT />
				<CONTAINER_ID>SEGU4585680</CONTAINER_ID>
				<OID>7</OID>
			</DECL_CONTAINER>
		</DECL_CONTAINERS>
		<DECL_CERTIFICATES>
			<DECL_CERTIFICATE>
				<OID>0</OID>
				<SEQ_NO />
				<DOCU_CODE>A</DOCU_CODE>
				<CERT_CODE>440450115005202000</CERT_CODE>
			</DECL_CERTIFICATE>
		</DECL_CERTIFICATES>
	</EnvelopBody>
</DeclEnvelop>

```
