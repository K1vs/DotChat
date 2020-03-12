export class CleanableValueSetter{
    constructor(target, cleanValue){
        this._target = target;
        this._cleaners = [];
        this._cleanValue = cleanValue;
    }

    add(name, value){
        if(this._target){
            this._target[name] = value;
            this._cleaners.push(() => {
                if(this._target[name] === value){
                    this._target[name] = this._cleanValue;
                }
            });
        }
    }

    clean(){
        this._cleaners.forEach(cleaner => cleaner());
    }
}