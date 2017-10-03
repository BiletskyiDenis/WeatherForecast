import { Component, Input,Output, EventEmitter } from "@angular/core"

@Component({
    selector: 'add-button',
    templateUrl: './addButton.component.html',
    styleUrls: ['./addButton.component.css'],
})
export class AddButton {

    @Input()
    center: boolean;

    @Output() onClickButton = new EventEmitter();

    onClick(): void {
        this.onClickButton.emit();
    }
}