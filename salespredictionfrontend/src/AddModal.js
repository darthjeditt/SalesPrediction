import React,{Component} from 'react';
import {Modal,Button, Row, Col, Form} from 'react-bootstrap';

export class AddModal extends Component{
    constructor(props){
        super(props);
        this.state={deps:[]};
        this.handleSubmit=this.handleSubmit.bind(this);
        // this.handleFileSelected=this.handleFileSelected.bind(this);
    }

    // photofilename = "anonymous.png";
    // imagesrc = process.env.REACT_APP_PHOTOPATH+this.photofilename;

    componentDidMount(){
        fetch(process.env.REACT_APP_API+'user')
        .then(response=>response.json())
        .then(data=>{
            this.setState({deps:data});
        });
    }

    handleSubmit(event){
        event.preventDefault();
        fetch(process.env.REACT_APP_API+'user',{
            method:'POST',
            headers:{
                'Accept':'application/json',
                'Content-Type':'application/json'
            },
            body:JSON.stringify({
                SalesId:null,
                Dos:event.target.Dos.value,
                Sales:event.target.Sales.value
            })
        })
        .then(res=>res.json())
        .then((result)=>{
            alert(result);
        },
        (error)=>{
            alert('Failed');
        })
    }


    // handleFileSelected(event){
    //     event.preventDefault();
    //     this.photofilename=event.target.files[0].name;
    //     const formData = new FormData();
    //     formData.append(
    //         "myFile",
    //         event.target.files[0],
    //         event.target.files[0].name
    //     );

    //     fetch(process.env.REACT_APP_API+'Employee/SaveFile',{
    //         method:'POST',
    //         body:formData
    //     })
    //     .then(res=>res.json())
    //     .then((result)=>{
    //         this.imagesrc=process.env.REACT_APP_PHOTOPATH+result;
    //     },
    //     (error)=>{
    //         alert('Failed');
    //     })
        
    // }

    render(){
        return (
            <div className="container">

<Modal
{...this.props}
size="lg"
aria-labelledby="contained-modal-title-vcenter"
centered
>
    <Modal.Header clooseButton>
        <Modal.Title id="contained-modal-title-vcenter">
            Import previous records of sales
        </Modal.Title>
    </Modal.Header>
    <Modal.Body>

        <Row>
            {/* <Col sm={6}>
                <Form onSubmit={this.handleSubmit}>
                    <Form.Group controlId="Sales">
                        <Form.Label>Sales</Form.Label>
                        <Form.Control type="float" name="Sales" required 
                        placeholder="Sales"/>
                    </Form.Group>

                    <Form.Group controlId="Dos">
                        <Form.Label>Date</Form.Label>
                        <Form.Control type="date" name="Dos" required
                        placeholder="Dos"
                        />
                       
                        
                    </Form.Group>

                    <Form.Group>
                        <Button variant="primary" type="submit">
                            Predict
                        </Button>
                    </Form.Group>
                </Form>
            </Col> */}

            <Col sm={6}>
                <Form onSubmit={this.handleSubmit}>
                <input onChange={this.handleFileSelected} type="File"/>
                </Form>
            </Col>
        </Row>
    </Modal.Body>
    
    <Modal.Footer>
        <Button variant="primary" type="submit">Import</Button>
        <Button variant="danger" onClick={this.props.onHide}>Close</Button>
    </Modal.Footer>

</Modal>

            </div>
        )
    }

}