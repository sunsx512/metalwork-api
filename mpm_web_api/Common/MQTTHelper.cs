using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Disconnecting;
using MQTTnet.Client.Options;
using MQTTnet.Client.Receiving;
using MQTTnet.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace mpm_web_api.Common
{
    public class MQTTHelper
    {
        public static MqttClient mqttClient;


        async public Task MqttClient_Subscribed()
        {
            var factory = new MqttFactory();        //声明一个MQTT客户端的标准步骤 的第一步
            mqttClient = factory.CreateMqttClient() as MqttClient;  //factory.CreateMqttClient()实际是一个接口类型（IMqttClient）,这里是把他的类型变了一下
            string clientId = Guid.NewGuid().ToString();
            try
            {

                var options = new MqttClientOptionsBuilder()
                     .WithTcpServer(GlobalVar.mqtthost, GlobalVar.mqttport)
                     .WithCredentials(GlobalVar.mqttuser, GlobalVar.mqttpwd)
                     .WithClientId(Guid.NewGuid()
                     .ToString().Substring(0, 5)).Build();

                //异步方法 ConnectAsync 来连接到服务端
                await mqttClient.ConnectAsync(options);
                mqttClient.ConnectedHandler = new MqttClientConnectedHandlerDelegate(new Func<MqttClientConnectedEventArgs, Task>(Connected));
                mqttClient.DisconnectedHandler = new MqttClientDisconnectedHandlerDelegate(new Func<MqttClientDisconnectedEventArgs, Task>(Disconnected));
                //mqttClient.ApplicationMessageReceivedHandler = new MqttApplicationMessageReceivedHandlerDelegate(new Action<MqttApplicationMessageReceivedEventArgs>(MqttApplicationMessageReceived));

                //await mqttClient.SubscribeAsync(new TopicFilter(topic, MqttQualityOfServiceLevel.AtLeastOnce));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Subscribe error:" + ex.Message);
            }
        }

        //private void MqttApplicationMessageReceived(MqttApplicationMessageReceivedEventArgs obj)
        //{
        //    throw new NotImplementedException();
        //}

        private Task Disconnected(MqttClientDisconnectedEventArgs arg)
        {
            Console.WriteLine("连接断开");
            throw new NotImplementedException();
        }

        private Task Connected(MqttClientConnectedEventArgs arg)
        {
            Console.WriteLine("连接成功");
            throw new NotImplementedException();
        }



        public async Task<bool> SendAsync(string content)
        {
            if (mqttClient == null)
                await MqttClient_Subscribed();
            if (!mqttClient.IsConnected)
               await MqttClient_Subscribed();
            await mqttClient.PublishAsync(GlobalVar.mqtttopic, content, MqttQualityOfServiceLevel.AtLeastOnce, true);
            return true;
        }
    }
}
