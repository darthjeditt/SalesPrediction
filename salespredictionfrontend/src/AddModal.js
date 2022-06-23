import React,{Component} from 'react';
import axios from 'axios';
import {Modal,Button, Row, Col, Form} from 'react-bootstrap';

export class AddModal extends Component{
    constructor(props){
        super(props);
        this.state={deps:[]};
        this.handleSubmit=this.handleSubmit.bind(this);
        this.handleFileSelected=this.handleFileSelected.bind(this);
    }

    fileState = {
        selectedFile: null
    }

    onFileChange = event => {
        this.setState({fileState: event.target.files[0]});
    }

    componentDidMount(){
        fetch(process.env.REACT_APP_API+'user')
        .then(response=>response.json())
        .then(data=>{
            this.setState({deps:data});
        });
    }

    handleSubmit(event){
        event.preventDefault();
        alert('Success');
        fetch(process.env.REACT_APP_API+'user')
        .then(response=>response.json())
        .then(data=>{
            this.setState({predsales:data});
        });
    }


    handleFileSelected(event){
        event.preventDefault();
        alert('importing data');
        const formData = new FormData();
        formData.append(
            "myFile",
            event.target.files
        );
        axios.post(process.env.REACT_APP_API+"user/SaveFile", formData);
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
            Import previous records of sales
        </Modal.Title>
    </Modal.Header>
    <Modal.Body>

        <Row>
            <Col sm={6}>
                <Form>
                    <Form.Group>
                    <input name='myFile' type="File" onChange={this.handleFileSelected}/>
                    </Form.Group>
                    <Button variant="primary" onClick={this.handleSubmit}>Import</Button>
                    &nbsp;&nbsp;&nbsp;
                    <Button variant="danger" onClick={this.props.onHide}>Close</Button>

                </Form>
            </Col>
        </Row>
        
    </Modal.Body>
</Modal>

            </div>
        )
    }

}