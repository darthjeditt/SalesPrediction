import React,{Component} from 'react';
import {Modal,Button, Row, Col, Form} from 'react-bootstrap';

export class EditModal extends Component{
    constructor(props){
        super(props);
        this.state={deps:[]};
        this.handleSubmit=this.handleSubmit.bind(this);
        this.handleFileSelected=this.handleFileSelected.bind(this);
    }

    photofilename = "anonymous.png";
    imagesrc = process.env.REACT_APP_PHOTOPATH+this.photofilename;

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
            method:'PUT',
            headers:{
                'Accept':'application/json',
                'Content-Type':'application/json'
            },
            body:JSON.stringify({
                SalesId:event.target.SalesId.value,
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
    
    test(event){
        event.preventDefault();
        alert('Success');

    }

    handleFileSelected(event){
        event.preventDefault();
        this.photofilename=event.target.files[0].name;
        const formData = new FormData();
        formData.append(
            "myFile",
            event.target.files[0],
            event.target.files[0].name
        );

        fetch(process.env.REACT_APP_API+'user',{
            method:'POST',
            body:formData
        })
        .then(res=>res.json())
        .then((result)=>{
            this.imagesrc=process.env.REACT_APP_PHOTOPATH+result;
        },
        (error)=>{
            alert('Failed');
        })
        
    }

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
            Predict Future Sales
        </Modal.Title>
    </Modal.Header>
    <Modal.Body>

        <Row>
            <Col sm={6}>
                <Form onSubmit={this.test}>
                <Form.Group controlId="Dos">
                        <Form.Label>Date to Predict</Form.Label>
                        <Form.Control type="date" name="Dos" required 
                        placeholder="Dos"
                        defaultValue={this.props.dos}/>
                    </Form.Group>
                    <Button variant="primary" type="submit">Predict Sales</Button>
                    &nbsp;&nbsp;&nbsp;
                    <Button variant="danger" onClick={this.props.onHide}>Close</Button>
                </Form>
            </Col>
        </Row>
    </Modal.Body>
    <Modal.Footer>
        <p id="this.props.dos"></p>
    </Modal.Footer>
</Modal>

            </div>
        )
    }

}