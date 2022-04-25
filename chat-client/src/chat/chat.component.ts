import { Component, OnInit } from "@angular/core";
import { ChatService } from "./chat.service";
import { Message } from "src/models/message";
import { AppService } from "src/app/app.service";


@Component({
    selector: 'chat',
    templateUrl: './chat.component.html',
    styleUrls: ['./chat.component.css'],
    providers: [ChatService, AppService]
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
        if(this.msgDto.text.length == 0){
          window.alert("Please enter message");
          return;
        } 
        else {
          this.chatService.broadcastMessage(this.msgDto);
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