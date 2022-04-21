import { Component, OnInit } from "@angular/core";
import { ChatService } from "./chat.service";
import { Message } from "src/models/message";


@Component({
    selector: 'chat',
    templateUrl: './chat.component.html',
    styleUrls: ['./chat.component.css'],
    providers: [ChatService]
})
export class ChatComponent implements OnInit{
    constructor(private chatService: ChatService) {
        
    }

    ngOnInit(): void {
      this.chatService.retrieveMappedObject().subscribe( (receivedObj: Message) => { this.addToInbox(receivedObj);});  // calls the service method to get the new messages sent
                                                       
    }
  
    msgDto: Message = new Message();
    msgInboxArray: Message[] = [];
  
    send(): void {
      if(this.msgDto) {
        if(this.msgDto.user.length == 0 || this.msgDto.user.length == 0){
          window.alert("Both fields are required.");
          return;
        } else {
          this.chatService.broadcastMessage(this.msgDto);                   // Send the message via a service
        }
      }
    }
  
    addToInbox(obj: Message) {
      let newObj = new Message();
      newObj.user = obj.user;
      newObj.text = obj.text;
      this.msgInboxArray.push(newObj);
    }
}